// Author : Auguste Paccapelo

using System.ComponentModel.DataAnnotations;

namespace Com.IsartDigital.Hackaton
{
    public enum StatType
    {
        [Display(Name ="Confort")] Confort,
        [Display(Name ="Social")] Social,
        [Display(Name ="Argent")] Argent,
        [Display(Name ="Tente")] Tent,
        [Display(Name ="Vêtement")] Cloting,
        [Display(Name ="Conserve")] Food,
        [Display(Name ="Kit de soins")] HealthKit,
        [Display(Name ="Réserve d'eau")] Water,
        [Display(Name ="Contact (Philippe)")] Contact1,
        [Display(Name ="Contact (Geneviève)")] Contact2
    }

    public enum CharactersEnum
    {
        OldLady,
        Student,
        Rich
    }

    public enum Cities
    {
        STRASBOURG,
        FREJUS,
        LAROCHELLE,
        NULL
    }
}
