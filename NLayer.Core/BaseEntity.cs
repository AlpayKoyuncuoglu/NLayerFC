﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    //abstract ile bu nesnenin örneğinin alınmasının önüne geçilmiştir
    //bir class entity olarak isimlendiriliyorsa veritabanında bir karşılığı var demektir
    public abstract class BaseEntity
    {
        //entityframework buradaki id'yi primarykey olarak algılar
        //ancak EntityId olarak yazım değiştirilirse [Key] ifadesi property üstüne eklenmelidir
        //bu yüzden custom isim vermekten kaçınılabilir
        public int Id { get; set; }
        public DateTime CreatedDate  { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
