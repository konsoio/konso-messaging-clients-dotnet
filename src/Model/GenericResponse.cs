 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Konso.Clients.Messagings.Model
{
    public class GenericResponse<T> where T : notnull
    {
        /// <summary>
        /// Returns errors in case of failure
        /// </summary>
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// Return validation errors
        /// </summary>
        public List<ValidationErrorItem> ValidationErrors { get; set; }


        public T Result { get; set; }


        [JsonIgnore]
        public bool Succeeded
        {
            get
            {
                var errors = Errors == null;
                if (!errors)
                {
                    errors = (Errors.Count == 0);
                }

                var validationErrors = ValidationErrors == null;
                if (!validationErrors)
                {
                    validationErrors = ValidationErrors.Count == 0;
                }

                return errors && validationErrors;
            }

        }
    }
}
