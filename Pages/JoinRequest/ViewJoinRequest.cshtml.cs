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
        public int tmID { get; set; }
        public string Username { get; set; }
        public string requestType { get; set; }
        public List<UserProfile> UserList { get; set; }
        public void OnGet(int ProfileID, int TeamID, int RequestID,int ApproverID)
        {

            SqlDataReader joinReader2 = DBTeamClass.TeamOwner(HttpContext.Session.GetString("username"),TeamID);
            SqlDataReader teamIDReader = DBJoinRequest.TeamIDReader(ProfileID);
            SqlDataReader requestReader = DBJoinRequest.RequestType(RequestID);
            while (teamIDReader.Read())
            {
                 tmID = Int32.Parse(teamIDReader["TeamID"].ToString());
            }
            while (requestReader.Read())
            {
                 requestType = requestReader["_Type"].ToString();
            }

            if (TeamID != 0 || ProfileID !=0)
            {
               
                    if (requestType.Equals("Join Request"))
                    {
                        DBAddToTeamClass.InsertUserTeamComposition(ProfileID, TeamID);
                        DBJoinRequest.UpdateJoinRequest("y", RequestID);
                       
                    }
                    else
                    {
                        DBAddToTeamClass.InsertUserTeamComposition(ApproverID, tmID);
                        DBJoinRequest.UpdateJoinRequest("y", RequestID);
                        

                    }
                
               
            }
            else
            {
                DBJoinRequest.UpdateJoinRequest("d",RequestID);
            }



        }
    }
}

    
