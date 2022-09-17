using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddUser
    {


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Position { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public List<string> Roles { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "������ ������ ���� �� ����� 6 ��������")]
        public string Password { get; set; }

        public AddUser()
        {
              Roles = new List<string>();
        }
    }
}