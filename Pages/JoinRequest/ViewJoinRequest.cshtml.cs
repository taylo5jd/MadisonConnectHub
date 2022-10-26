using lab_1_part_3.Pages.DBClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab_1_part_3.Pages.JoinRequest
{
    public class ViewJoinRequestModel : PageModel
    {
        public int TeamID { get; set; }
        public int ProfileID { get; set; }
        public List<UserProfile> UserList { get; set; }
        public void OnGet(int ProfileID, int TeamID, int RequestID)
        {

            SqlDataReader joinReader2 = DBTeamClass.TeamOwner(HttpContext.Session.GetString("username"),TeamID);


            if (TeamID != 0)
            {
                while (joinReader2.Read())
                {
                    DBAddToTeamClass.InsertUserTeamComposition(ProfileID, TeamID);
                   DBJoinRequest.UpdateJoinRequest("y", RequestID);
                    break;
                }
               
            }
            else
            {
                DBJoinRequest.UpdateJoinRequest("d",RequestID);
            }



        }
    }
}

    
