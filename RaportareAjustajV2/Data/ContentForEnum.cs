using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaportareAjustajV2
{
    public class ContentForEnum
    {
    }

    public enum UtilajeAjustaj
    {
        Elti,
        Elind,
        Fierastraie,
        PresaValdora,
        RullatriceLandgraf,
        RullatriceProjectMan,
        PellatriceLandgraf,
        Novaflux,
        Gadda,
        Toate,
        PresaDunke,
        UsBar,
        UsBlum,
        CalitateOtel
    }

    public enum TratamentTermic
    {
        N,
        A,
        FP,
        SR,
        QT
    }

    public enum Furnizor
    {
        VA,
        TK,
        SW,
        SH,
        FG,
        RE,
        AM,
        BD,
        BE,
        BM,
        BT,
        CA,
        CR,
        DP,
        EZ,
        FB,
        FV,
        LT,
        LU,
        ME,
        MI,
        NP,
        PO,
        PS,
        RO,
        SK,
        TE,
        CK,
        BB,
        SO
    }

    public enum StareMaterial
    {
        LAMINAT,
        IQT,
        DQT,
        RULAT,
        PELAT,
        SABLAT,
        QT,
        N,
        A,
        FP,
        SR
    }

    public enum TipDiscontinuitate
    {
        DCE,
        DCP,
        DCG,
        PC,
        DCEG,
        DCPI,
        DCE_PC,
        SS
    }

    public enum Motiv
    {
        CB,
        NOVA,
        PEL,
        RUL,
        SR,
        TQT,
        TA,
        TFP,
        TN,
        TTQT,
        TTA,
        TTFP,
        TTN,
        MAGNAFLUX
    }

}
