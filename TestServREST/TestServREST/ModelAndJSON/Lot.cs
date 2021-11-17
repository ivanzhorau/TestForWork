using System;
using System.ComponentModel.DataAnnotations;

namespace TestServREST
{
    public class Lot
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Подпись не должна быть пустой")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Должна быть ссылка на изображение")]
        public string ImagePath { get; set; }
    }
}
