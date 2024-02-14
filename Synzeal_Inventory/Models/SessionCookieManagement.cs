using System;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class SessionCookieManagement
    {

        public enum SessionCookieVariable
        {
            UserId,
            UserName,
            Name,
            UserEmail,
            UserRoll,
            RedirectBackAdmin,
            IsAdmin,
            IsScientist,
            IsSubScientist,
            IsLogin,
            IsInventory,
            IsQuote,
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
            IsAnalytical,
            IsQC,
            IsMasterAdmin,
            IsOTPValidate,
            CountryId,
            IsInternational,
            IsControlledSubstance,
            DefaultCurrencyId,
            IsDocumentation,
            IsDomesticDispatch,
            IsExportDispatch,
            IsSupport,
            IsGLP
            //,RoleType,
            //UserTitle,
            //IsShowPupil
        }
        public static bool IsLogin
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsLogin.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsLogin.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsOTPValidate
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsOTPValidate.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsOTPValidate.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsInventory
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsInventory.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsInventory.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsPrice
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsPrice.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsPrice.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsMiniAdmin
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsMiniAdmin.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsMiniAdmin.ToString(), Convert.ToString(value));
            }
        }

        public static bool IsMasterAdmin
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsMasterAdmin.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsMasterAdmin.ToString(), Convert.ToString(value));
            }
        }

        public static bool IsQuote
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsQuote.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsQuote.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsProject
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsProject.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsProject.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsDispatch
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsDispatch.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsDispatch.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsDomesticDispatch
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsDomesticDispatch.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsDomesticDispatch.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsExportDispatch
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsExportDispatch.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsExportDispatch.ToString(), Convert.ToString(value));
            }
        }
        
        public static bool IsInvoice
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsInvoice.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsInvoice.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsProjectLogin
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsProjectLogin.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsProjectLogin.ToString(), Convert.ToString(value));
            }
        }

        public static bool IsDocumentation
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsDocumentation.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsDocumentation.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsPendingUpload
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsPendingUpload.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsPendingUpload.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsAdmin
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsAdmin.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsAdmin.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsClient
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsClient.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsClient.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsPurchase
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsPurchase.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsPurchase.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsFollowUp
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsFollowUp.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsFollowUp.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsQC
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsQC.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsQC.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsInternational
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsInternational.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsInternational.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsControlledSubstance
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsControlledSubstance.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsControlledSubstance.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsGLP
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsGLP.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsGLP.ToString(), Convert.ToString(value));
            }
        }
        public static bool IsScientist
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsScientist.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsScientist.ToString(), Convert.ToString(value));
            }
        }

        public static bool IsSubScientist
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsSubScientist.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsSubScientist.ToString(), Convert.ToString(value));
            }
        }

        public static bool IsSupport
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsSupport.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsSupport.ToString(), Convert.ToString(value));
            }
        }


        public static bool IsAnalytical
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.IsAnalytical.ToString());
                if (value == null)
                {
                    return false;
                }
                return Convert.ToBoolean(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.IsAnalytical.ToString(), Convert.ToString(value));
            }
        }

        public static string UserName
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.UserName.ToString());
                if (value == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.UserName.ToString(), Convert.ToString(value));
            }
        }

        public static string Name
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.Name.ToString());
                if (value == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.Name.ToString(), Convert.ToString(value));
            }
        }

        public static string UserEmail
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.UserEmail.ToString());
                if (value == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.UserEmail.ToString(), Convert.ToString(value));
            }
        }

        public static string UserRoll
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.UserRoll.ToString());
                if (value == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.UserRoll.ToString(), Convert.ToString(value));
            }
        }

        public static int DefaultCurrencyId
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.DefaultCurrencyId.ToString());
                if (value == null)
                {
                    return 1;
                }
                return Convert.ToInt32(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.DefaultCurrencyId.ToString(), Convert.ToString(value));
            }
        }
        

        public static int UserId
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.UserId.ToString());
                if (value == null)
                {
                    return 0;
                }
                return Convert.ToInt32(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.UserId.ToString(), Convert.ToString(value));
            }
        }

        public static int CountryId
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.CountryId.ToString());
                if (value == null)
                {
                    return 0;
                }
                return Convert.ToInt32(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.CountryId.ToString(), Convert.ToString(value));
            }
        }
        

        public static bool CheckAdmin()
        {
            if (Common.GetCookie(SessionCookieVariable.UserName.ToString()) != null)
            {
                if (!string.IsNullOrEmpty(Common.GetCookie(SessionCookieVariable.UserName.ToString())))
                {
                    if (Convert.ToString(Common.GetCookie(SessionCookieVariable.UserName.ToString())) == "Admin")
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
                if (HttpContext.Current.Session[SessionCookieManagement.RedirectBackAdmin.ToString()] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[SessionCookieManagement.RedirectBackAdmin.ToString()].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionCookieManagement.RedirectBackAdmin.ToString()] = value;
            }
        }
        public static string LoginCompanyName
        {
            get
            {
                var value = Common.GetCookie(SessionCookieVariable.LoginCompanyName.ToString());
                if (value == null)
                {
                    return string.Empty;
                }
                return Convert.ToString(value);
            }
            set
            {
                Common.SetCookie(SessionCookieVariable.LoginCompanyName.ToString(), Convert.ToString(value));
            }
        }
    }
}