using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;


namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class AltGrupIleAyniGunNobetDurumu : IComplexType
    {
        public int NobetAltGrupId { get; set; }
        public int TakvimId { get; set; }
        public DateTime Tarih { get; set; }
        public int EczaneNobetGrupIdAltGrubuOlmayan { get; set; }
        public int EczaneNobetGrupIdAltGruplu { get; set; }

        public int EczaneIdAltGrubuOlmayan { get; set; }
        public int EczaneIdAltGruplu { get; set; }
        public int NobetGrupIdAltGrubuOlmayan { get; set; }
        public int NobetGrupIdAltGruplu { get; set; }

        public string EczaneAdiAltGrubuOlmayan { get; set; }
        public string EczaneAdiAltGruplu { get; set; }
        public string NobetGrupAdiAltGrubuOlmayan { get; set; }
        public string NobetGrupAdiAltGruplu { get; set; }

        public string GunGrup { get; set; }
    }
}

