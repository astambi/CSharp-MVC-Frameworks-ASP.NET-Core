namespace CameraBazaar.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    // allows multiple selection! enum values => powers of 2!
    // https://www.dotnetperls.com/enum-flags

    [Flags]
    public enum LightMetering
    {
        Spot = 1,
        [Display(Name = "Center Weighted")]
        CenterWeighted = 2,
        Evaluative = 4
    }
}
