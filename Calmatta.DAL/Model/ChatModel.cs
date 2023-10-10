using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calmatta.DAL.Model
{
    /// <summary>
    /// Input chat model
    /// </summary>
    public class ChatModel
    {
        /// <summary>
        /// Message text
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}
