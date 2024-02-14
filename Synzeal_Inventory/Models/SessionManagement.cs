using System;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class SessionManagement
    {

        public enum SessionVariable
        {
            UserId,
            UserName,
            UserEmail,
            UserRoll,
            RedirectBackAdmin,
            IsAdmin,
            IsScientist,
            IsSubScientist,
            IsLogin,
            IsInventory,
            IsPrice,
            IsProject,
            IsDispatch,
            IsInvoice,
            IsPurchase,
            IsClient,
            IsPendingUpload,
            IsProjectLogin,
            LoginCompanyName,
            IsFollowUp,
            IsMiniAdmin,
            IsAnalytical
            //,RoleType,
            //UserTitle,
            //IsShowPupil
        }
        public static bool IsLogin
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsLogin.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsLogin.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsLogin.ToString()] = value;
            }
        }
        public static bool IsInventory
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsInventory.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsInventory.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsInventory.ToString()] = value;
            }
        }
        public static bool IsPrice
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsPrice.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsPrice.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsPrice.ToString()] = value;
            }
        }
        public static bool IsMiniAdmin
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsMiniAdmin.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsMiniAdmin.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsMiniAdmin.ToString()] = value;
            }
        }
        public static bool IsProject
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsProject.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsProject.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsProject.ToString()] = value;
            }
        }
        public static bool IsDispatch
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsDispatch.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsDispatch.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsDispatch.ToString()] = value;
            }
        }
        public static bool IsInvoice
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsInvoice.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsInvoice.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsInvoice.ToString()] = value;
            }
        }
        public static bool IsProjectLogin
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsProjectLogin.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsProjectLogin.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsProjectLogin.ToString()] = value;
            }
        }
        public static bool IsPendingUpload
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsPendingUpload.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsPendingUpload.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsPendingUpload.ToString()] = value;
            }
        }
        public static bool IsAdmin
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsAdmin.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsAdmin.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsAdmin.ToString()] = value;
            }
        }
        public static bool IsClient
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsClient.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsClient.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsClient.ToString()] = value;
            }
        }
        public static bool IsPurchase
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsPurchase.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsPurchase.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsPurchase.ToString()] = value;
            }
        }
        public static bool IsFollowUp
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsFollowUp.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsFollowUp.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsFollowUp.ToString()] = value;
            }
        }
        public static bool IsScientist
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsScientist.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsScientist.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsScientist.ToString()] = value;
            }
        }

        public static bool IsSubScientist
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsSubScientist.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsSubScientist.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsSubScientist.ToString()] = value;
            }
        }
        

        public static bool IsAnalytical
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.IsAnalytical.ToString()] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsAnalytical.ToString()].ToString());
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.IsAnalytical.ToString()] = value;
            }
        }

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.UserName.ToString()] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[SessionVariable.UserName.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.UserName.ToString()] = value;
            }
        }

        public static string UserEmail
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.UserEmail.ToString()] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[SessionVariable.UserEmail.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.UserEmail.ToString()] = value;
            }
        }

        public static string UserRoll
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.UserRoll.ToString()] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[SessionVariable.UserRoll.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.UserRoll.ToString()] = value;
            }
        }

        public static int UserId
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.UserId.ToString()] == null)
                {
                    return 0;
                }
                return (int)HttpContext.Current.Session[SessionVariable.UserId.ToString()];
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.UserId.ToString()] = value;
            }
        }

        public static bool CheckAdmin()
        {
            if (HttpContext.Current.Session["UserName"] != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserName"])))
                {
                    if (Convert.ToString(HttpContext.Current.Session["UserName"]) == "Admin")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public static bool CheckISams()
        {
            if (HttpContext.Current.Session["UserName"] != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserName"])))
                {
                    if (Convert.ToString(HttpContext.Current.Session["UserName"]) == "Isams")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool CheckParent()
        {
            if (HttpContext.Current.Session["UserTypeId"] != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserTypeId"])))
                {
                    if (Convert.ToString(HttpContext.Current.Session["UserTypeId"]) == "2")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string RedirectBackAdmin
        {
            get
            {
                if (HttpContext.Current.Session[SessionVariable.RedirectBackAdmin.ToString()] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[SessionVariable.RedirectBackAdmin.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.RedirectBackAdmin.ToString()] = value;
            }
        }
        public static string LoginCompanyName
        {
             get
            {
                if (HttpContext.Current.Session[SessionVariable.LoginCompanyName.ToString()] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[SessionVariable.LoginCompanyName.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionVariable.LoginCompanyName.ToString()] = value;
            }
        }
        //public static bool IsShowPupil
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[SessionVariable.IsShowPupil.ToString()] == null)
        //        {
        //            return false;
        //        }
        //        return Convert.ToBoolean(HttpContext.Current.Session[SessionVariable.IsShowPupil.ToString()].ToString());
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[SessionVariable.IsShowPupil.ToString()] = value;
        //    }
        //}
    }
}