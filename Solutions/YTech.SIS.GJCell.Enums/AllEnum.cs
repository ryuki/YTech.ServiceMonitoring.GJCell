using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.SIS.GJCell.Enums
{
    public enum CustomerType
    {
        Toko,
        Pribadi
    }

    public enum EnumWOStatus
    {
        Baru_Masuk,
        Sedang_Dikerjakan,
        Menunggu_Persetujuan,
        Sudah_Setuju,
        Menunggu_Part,
        Kirim_ke_SC,
        Terima_dari_SC,
        Selesai,
        Cancel_Teknisi,
        Cancel_Konsumen,
        Closed
    }

    public enum EnumSCToko
    {
        Toko,
        SC
    }

    public enum EnumPriority
    {
        Normal,
        Urgent
    }

    public enum EnumReferenceType
    {
        WONo
    }

    public enum EnumReports
    {
        RptWODailyRecap,
        RptPrintWOFactur,
        RptMasterCustomer,
        RptMasterEmp,
        RptMasterUnitType,
        RptMasterUnitMerk,
        RptMasterSPart,
        RptMasterEquip,
        RptWOTrack,
        RptWOSpartUsed,
        RptPrintWOFacturEvercoss,
        RptPrintWOFacturAdvan,
        RptPrintWOFacturGJCell
    }

    public enum EnumWOLog
    {
        Save,
        Update,
        Delete,
        Read,
        Print,
        Mutation
    }

    public enum EnumWOSPartStatus
    {
        Request,
        Serah_Terima_dan_Gunakan,
        Retur
    }
}
