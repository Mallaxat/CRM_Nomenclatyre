using CRM_Nomenclatyre.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CRM_Nomenclatyre.Servises
{
    public class mSettings
    {
        
        private static mSettings _instance;

        public readonly MessageeServise serviseMessege;
        public readonly WindowService serviseWindow;

        public Users user;

        private mSettings(MessageeServise serviseMessege,WindowService serviseWindow,Users user) 
        {
            this.serviseMessege = serviseMessege;
            this.serviseWindow = serviseWindow;
            this.user = user;
        }

        public static mSettings Initialize(MessageeServise serviseMessege, WindowService serviseWindow, Users user)
        {
            if (_instance == null)
            {
                _instance = new mSettings(serviseMessege, serviseWindow, user);
            }

            return _instance;
        }

        public static string Rand(int count, bool var = true)
        {
            Random rnd = new Random();
            string result;
            while (true)
            {
                result = String.Empty;
                for (int i=0;i<count;i++)
                {
                    result += rnd.Next(0, 9);
                }
               if(var)
                {
                    if (!SqlService.ArticulSQL.FindBarcode(result)) 
                        return result;

                }
                else 
                {
                    if (!SqlService.ArticulSQL.FindArticul(result))
                        return result;
                }
            }
        }

        public class RequiredValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            {
                return string.IsNullOrWhiteSpace(value?.ToString())
                    ? new ValidationResult(false, "Артикул обязателен")
                    : ValidationResult.ValidResult;
            }
        }
    }


    }


