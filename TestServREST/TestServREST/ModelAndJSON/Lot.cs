using System;
using System.ComponentModel.DataAnnotations;

namespace TestServREST
{
    public class Lot
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "������� �� ������ ���� ������")]
        public string Title { get; set; }
        [Required(ErrorMessage = "������ ���� ������ �� �����������")]
        public string ImagePath { get; set; }
    }
}
