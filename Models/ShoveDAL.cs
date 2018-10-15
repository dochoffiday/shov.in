using System;
using System.Linq;
using AJ.UtiliTools;
using Shovin.DB;

namespace Shovin.Models
{
    public class ShoveDAL
    {
        public static Result<Shove> GetByTinyUrl(String TinyUrl)
        {
            Result<Shove> result = new Result<Shove>();

            try
            {
                using (ShovinDataContext db = new ShovinDataContext())
                {
                    Shove shove = (from q in db.Shoves where String.Compare(q.TinyUrl, TinyUrl, false) == 0 select q).FirstOrDefault();

                    if (shove != null)
                    {
                        result = new Result<Shove>(shove);
                    }
                    else
                    {
                        result = new Result<Shove>(1, "Tiny Url Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                result = new Result<Shove>(2, ex.Message);
            }

            return result;
        }

        public static Result<Shove> Create(String FullUrl, String Keyword, String CreatedBy)
        {
            if (!Keyword.IsNullOrEmpty())
            {
                if (GetByTinyUrl(Keyword).Data != null)
                {
                    return new Result<Shove>(1, "Tiny Url Already Exists");
                }
            }

            if (FullUrl.IsNullOrEmpty())
            {
                return new Result<Shove>(3, "Full Url Must Not Be Empty!");
            }

            Result<Shove> result = new Result<Shove>();

            try
            {
                using (ShovinDataContext db = new ShovinDataContext())
                {
                    Shove shove = new Shove();

                    shove.AdDiplayMilliseconds = 0;
                    shove.Created = DateTime.UtcNow;
                    shove.CreatedBy = CreatedBy;
                    shove.FullUrl = FullUrl;
                    shove.Keyword = Keyword;
                    shove.TinyUrl = "";

                    db.Shoves.InsertOnSubmit(shove);
                    db.SubmitChanges();

                    shove.TinyUrl = Shovin.Url.TinyUrl(shove.ShoveID);

                    db.SubmitChanges();

                    result = new Result<Shove>(shove);
                }
            }
            catch (Exception ex)
            {
                result = new Result<Shove>(2, ex.Message);
            }

            return result;
        }

        public static void LogVisit(String TinyUrl, String CreatedBy, String ReferrerUrl)
        {
            Result<Shove> shove = GetByTinyUrl(TinyUrl);

            if (shove.ErrorCode == 0 && shove.Data != null)
            {
                using (ShovinDataContext db = new ShovinDataContext())
                {
                    Visit visit = new Visit();

                    visit.Created = DateTime.UtcNow;
                    visit.CreatedBy = CreatedBy;
                    visit.ShoveID = shove.Data.ShoveID;
                    visit.ReferrerUrl = ReferrerUrl;

                    db.Visits.InsertOnSubmit(visit);
                    db.SubmitChanges();
                }
            }
        }
    }
}