using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.API.Dtos
{
  public  class StateRegistrationViewModel
    {


        public int id { get; set; }
         
        public string StateName { get; set; }
       
       //public virtual CountryRegistration CountryRegistration { get; set; }
        //public virtual  CountryRegistration CountryRegistration { get; set; }

    }
}
