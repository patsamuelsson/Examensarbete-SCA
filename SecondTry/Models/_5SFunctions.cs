using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{

    /* Model for keeping track of tasks that are done and what tasks still needed to be done.
       Sets buttons to disabled if another shift is selected */

    public class _5SFunctions
    {
        private dbContext context;
        public IEnumerable<GetShift5S> _5sDone;
        public IEnumerable<GetShift5S> _5sTodo;
        public string submitClass;
        public string canSubmit;
        public string[] links;
        public int numDone;
        public int numTodo;

        public _5SFunctions()
        {
            this.context = new dbContext();
            links = new string[9]{
                "Avd A\\F01\\",
                "Avd A\\F02\\",
                "Avd B\\F03\\",
                "Avd B\\F04\\",
                "Avd C\\F05\\",
                "Avd C\\F07\\",
                "Avd D\\M11\\",
                "Avd D\\M12\\",
                "Avd E\\M9\\",
            };
        }

        public void set5S(loggedInUser user, int id, string comment, string status) {
            var s5 = (from s in context.GetShift5S where s._5SDoneID == id select s).First();
            s5.Performed = true;
            s5.PerformedBy = user.currShift.shift;
            s5.PerformedTime = DateTime.Now;
            s5.Comment = comment;
            s5.Status = status;
            context.SaveChanges();
        }

        /* Update the tasks done and todo from the user */
        public void update5S(loggedInUser user) {
            if(user.maskin != "RVL") { 
                int shift = rmvLast(user.selShift.shift);
                _5sDone = (from s in context.GetShift5S where s.Machine == user.maskin && s.PerformedBy == user.selShift.shift select s).AsEnumerable().Where(s => rmvLast(s.shift) <= shift && rmvLast(s.Duration) >= shift);
                _5sTodo = (from s in context.GetShift5S where s.Machine == user.maskin && s.Performed == false select s).AsEnumerable().Where(s => rmvLast(s.shift) <= shift && rmvLast(s.Duration) >= shift);
                numDone = _5sDone.Count();
                numTodo = _5sTodo.Count();
                if (user.currShift.shift != user.selShift.shift || user.userlevel == "gast")
                {
                    submitClass = "btn btn-default disabled";
                    canSubmit = "disabled";
                }
                else
                {
                    submitClass = "btn btn-default";
                    canSubmit = "";
                }
            }
        }

        /* Remove the last letter in the string (the shift) sent in and return the integer part */
        private int rmvLast(string str)
        {
            return int.Parse(str.Substring(0, 6));
        }

        /* Get Data_uri, convert to image and save to disk */
        public void saveImage(string id, string uri, loggedInUser user) {
            string hyperlink;
            uri = uri.Split(',')[1];
            byte[] imageBytes = Convert.FromBase64String(uri);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length)) {
                Image image = Image.FromStream(ms, true);
                string partLink = "";
                switch (user.Machno) {
                    case 1:
                        partLink = links[0];
                        break;
                    case 2:
                        partLink = links[1];
                        break;
                    case 3:
                        partLink = links[2];
                        break;
                    case 4:
                        partLink = links[3];
                        break;
                    case 5:
                        partLink = links[4];
                        break;
                    case 7:
                        partLink = links[5];
                        break;
                    case 9:
                        partLink = links[8];
                        break;
                    case 11:
                        partLink = links[6];
                        break;
                    case 12:
                        partLink = links[7];
                        break;
                    default:
                        break;
                }
                hyperlink = "\\\\s0000291\\archive\\Function\\1-Produktion\\Produktionsteam\\"+partLink+"5S_Säkerhetsrond\\Bilder\\Felaktigheter\\IMG"+id+"_"+DateTime.Now.ToString("yyMMdd")+".jpg";
                try
                {
                    image.Save(hyperlink, ImageFormat.Jpeg);
                }
                catch (Exception e) {
                    user.errorText = "Error: Could not save Image";
                }
            }
            int nId = int.Parse(id);
            var _5sTask = (from s in context.GetShift5S where s._5SDoneID == nId select s).First();
            _5sTask.HyperLink = hyperlink;
            context.SaveChanges();
        }
    }
}