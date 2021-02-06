using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.Klasis.Models
{
    public enum MutationTypeEnum
    {
        In,
        Out
    }

    public static class MutationCodes
    {
        public static readonly string ATL = "ATL";
        public static readonly string ATD1 = "ATD-1";
        public static readonly string ATD2 = "ATD-2";
        public static readonly string ATIS = "ATIS";
        public static readonly string ATIOT = "ATIOT";
        public static readonly string ATPS = "ATPS";
        public static readonly string ATP1 = "ATP-1";
        public static readonly string ATP2 = "ATP-2";
        public static readonly string ExDKH1 = "Ex.DKH-1";
        public static readonly string ExDKH2 = "Ex.DKH-2";
        public static readonly string ExDKH3 = "Ex.DKH-3";
        public static readonly string ExDKH4 = "Ex.DKH-4";

        public static readonly string AKK1 = "AKK-1";
        public static readonly string AKK2 = "AKK-2";
        public static readonly string AKK3 = "AKK-3";
        public static readonly string DKH1 = "DKH-1";
        public static readonly string DKH2 = "DKH-2";
        public static readonly string DKH3 = "DKH-3";
        public static readonly string DKH4 = "DKH-4";
        public static readonly string AKM1 = "AKM-1";
        public static readonly string AKM2 = "AKM-2";
    }

    public class MemberMutation
    {
        public string Code { get; private set; }
        public string Description { get; private set; }
        public MutationTypeEnum MutationType { get; private set; }

        private static Lazy<IReadOnlyList<MemberMutation>> memberMutations = new Lazy<IReadOnlyList<MemberMutation>>( () => GetData() );

        private MemberMutation(string code, string description, MutationTypeEnum mutationType)
        {
            Code = code;
            Description = description;
            MutationType = mutationType;
        }

        private static IReadOnlyList<MemberMutation> GetData()
        {
            var list = new List<MemberMutation>();

            list.Add(new MemberMutation(MutationCodes.ATL, "Angka pertambahan karena kelahiran", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATD1, "Angka pertambahan karena Baptis Kudus Dewasa - Dari bukan keluarga Kristen (percaya baru)", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATD2, "Angka pertambahan karena Baptis Kudus Dewasa - Dari keluarga Kristen ( belum baptis anak )", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATIS, " Angka pertumbuhan hasil inkubasi iman lintas generasi melalui Sidi", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATIOT, "Angka pertambahan anak baru, karena ikut Baptis bersama-sama orang tuanya", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATPS, "Angka pertambahan karena pindah dari gereja lain melalui Sidi", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATP1, "Angka pertambahan, karena pindah dari gereja lain ( Atestasi Masuk Dewasa )", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ATP2, "Angka pertambahan, karena pindah dari gereja lain ( Atestasi Masuk Anak )", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ExDKH1, "Inverse DKH-1", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ExDKH2, "Inverse DKH-2", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ExDKH3, "Inverse DKH-3", MutationTypeEnum.In));
            list.Add(new MemberMutation(MutationCodes.ExDKH4, "Inverse DKH-4", MutationTypeEnum.In));

            list.Add(new MemberMutation(MutationCodes.AKK1, "Angka pengurangan, karena pindah keanggotaan ( Atestasi keluar ) - Penyebab wajar", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.AKK2, "Angka pengurangan, karena pindah keanggotaan ( Atestasi keluar ) - Daya dorong dari dalam / daya tarik dari luar", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.AKK3, "Angka pengurangan, karena pindah keanggotaan ( Atestasi keluar anak ) - Ikut ortu", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.DKH1, "Alamat tidak diketahui, luar kota, luar negeri", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.DKH2, "Pindah Agama", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.DKH3, "Pindah tanpa Atestasi", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.DKH4, "Anggota Baptis Anak Hilang", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.AKM1, "Anggota Dewasa Meninggal", MutationTypeEnum.Out));
            list.Add(new MemberMutation(MutationCodes.AKM2, "Anggota Anak Meninggal", MutationTypeEnum.Out));

            return list;
        }

        public static IEnumerable<MemberMutation> Instance => memberMutations.Value;
    }
}
