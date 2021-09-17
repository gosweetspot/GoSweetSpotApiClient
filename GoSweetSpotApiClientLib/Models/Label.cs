using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    class Label
    {
    }

    public enum LabelFormat
    {
        LABEL_PNG = 1, // LEGACY
        LABEL_PDF = 2, // LEGACY
        COMMERCIAL_INVOICE = 3,
        LABEL_PNG_100X175 = 4,
        LABEL_PNG_100X150 = 5,
        LABEL_PDF_100X175 = 6,
        LABEL_PDF_100X150 = 7,
        LABEL_PDF_LABELOPE = 8,
        DG_FORM_PDF = 10,
        LABEL_ZPL_100X150 = 11,
        LABEL_ZPL_100X175 = 12,
        USER_CONFIGURED = 13,
        GOPRINT_PRN = 14,
        UNKNOWN = 0
    }
}
