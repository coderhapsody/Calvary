using Calvary.ViewModels.Activities.MemberVisit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Member
{
    public class CreateEditViewModel
    {
        public int Id { get; set; } //(int, not null)

        [Display(Name = "No. Anggota")]
        [Required(ErrorMessage = "Nomor Anggota harus diinput")]
        public string MemberNo { get; set; } //(varchar(20), not null)

        [Display(Name = "Nama")]
        [Required(ErrorMessage = "Nama harus diinput")]
        public string Name { get; set; } //(varchar(100), not null)

        [Display(Name = "Nama Panggilan")]
        [Required(ErrorMessage = "Nama Panggilan harus diinput")]
        public string NickName { get; set; } //(varchar(50), not null)

        [Display(Name = "Jenis Kelamin")]
        [Required(ErrorMessage = "Jenis Kelamin harus diinput")]
        public string Gender { get; set; } //(varchar(1), not null)

        [Display(Name = "Wilayah")]
        public int? RegionId { get; set; } //(int, null)

        [Display(Name = "Gol. Darah")]
        public string BloodType { get; set; } //(varchar(1), null)

        public string Rhesus { get; set; }

        [Display(Name = "Status Nikah")]
        public string MaritalStatus { get; set; } //(varchar(1), null)

        [Display(Name = "Atestasi Masuk")]
        public DateTime? JoinDate { get; set; } //(date, null)

        [Display(Name = "Atestasi Masuk Dari")]
        public int? JoinFromShrineId { get; set; } //(date, null)

        [Display(Name = "ATPS")]
        public bool JoinFlagATPS { get; set; }

        [Display(Name = "Atestasi Keluar")]
        public DateTime? ResignDate { get; set; } //(date, null)

        [Display(Name = "Alasan At. Keluar")]
        public int? ResignReasonId { get; set; }

        [Display(Name = "Atestasi Keluar Ke")]
        public int? ResignToShrineId { get; set; } //(date, null)

        [Display(Name = "Alamat")]
        public string Address { get; set; } //(varchar(100), null)

        [Display(Name = "Tlp. Rumah")]
        public string HomePhone { get; set; } //(varchar(20), null)

        [Display(Name = "Handphone 1")]
        public string CellPhone1 { get; set; } //(varchar(20), null)

        [Display(Name = "Handphone 2")]
        public string CellPhone2 { get; set; } //(varchar(20), null)

        [Display(Name = "Email")]
        public string Email { get; set; } //(varchar(100), null)

        [Display(Name = "Tgl. Lahir")]
        public DateTime? BirthDate { get; set; } //(date, null)

        [Display(Name = "Kota")]
        public string BirthCity { get; set; } //(varchar(50), null)

        [Display(Name = "Kantor")]
        public string OfficeName { get; set; } //(varchar(50), null)

        [Display(Name = "Alamat Kantor")]
        public string OfficeAddress { get; set; } //(varchar(100), null)

        [Display(Name = "Tlp. Kantor")]
        public string OfficePhone { get; set; } //(varchar(20), null)

        [Display(Name = "Kota")]
        public string OfficeCity { get; set; } //(varchar(50), null)

        [Display(Name = "Jabatan")]
        public string OfficeRole { get; set; } //(varchar(50), null)

        [Display(Name = "Pekerjaan")]
        public int? JobId { get; set; } //(int, null)

        [Display(Name = "Pendidikan Terakhir")]
        public int? LastEducationGradeId { get; set; } //(int, null)

        [Display(Name = "Bidang Pendidikan")]
        public int? EducationMajorId { get; set; } //(int, null)
        public int? SkillId1 { get; set; } //(int, null)
        public int? SkillId2 { get; set; } //(int, null)
        public int? SkillId3 { get; set; } //(int, null)
        public int? HobbyId1 { get; set; } //(int, null)
        public int? HobbyId2 { get; set; } //(int, null)
        public int? HobbyId3 { get; set; } //(int, null)

        [Display(Name = "Baptis Dewasa/Sidi")]
        public string ChrismationType { get; set; } //(varchar(1), null)

        public int? BaptizedReasonId { get; set; } // must be specified if ChrismationType=="B"

        [Display(Name = "Tempat Baptis/Sidi")]
        public int? ChrismationShrineId { get; set; } //(int, null)

        [Display(Name = "Baptis/Sidi Oleh")]
        public string ChrismationPriest { get; set; } //(varchar(100), null)

        [Display(Name = "Tgl. Baptis/Sidi")]
        public DateTime? ChrismationDate { get; set; } //(date, null)

        [Display(Name = "Akte Baptis/Sidi")]
        public string ChrismationCert { get; set; } //(varchar(50), null)

        [Display(Name = "Flag ATIS")]
        public bool ChrismationFlagATIS { get; set; }

        [Display(Name = "Nama Pasangan")]
        public string SpouseName { get; set; } //(varchar(100), null)

        [Display(Name = "No. Anggota Pasangan")]
        public string SpouseMemberNo { get; set; } //(varchar(20), null)

        [Display(Name = "Tgl Nikah")]
        public DateTime? MarriageDate { get; set; } //(date, null)

        [Display(Name = "Akte Nikah")]
        public string MarriageCert { get; set; } //(varchar(100), null)

        [Display(Name = "Kota")]
        public string MarriageCity { get; set; } //(varchar(50), null)

        [Display(Name = "Tempat Nikah")]
        public int? MarriageShrineId { get; set; } //(int, null)

        [Display(Name = "Pemberkatan Nikah Oleh")]
        public string MarriagePriest { get; set; } //(varchar(100), null)

        [Display(Name = "Tgl. Nikah")]
        public DateTime? MatrimonyDate { get; set; } //(date, null)

        [Display(Name = "Nama Ayah")]
        public string FatherName { get; set; } //(varchar(100), null)

        [Display(Name = "Status Baptis/Sidi Ayah")]
        public string FatherChrismationType { get; set; } //(varchar(1), null)

        [Display(Name = "No. Anggota Ayah")]
        public string FatherMemberNo { get; set; } //(varchar(20), null)

        [Display(Name = "Nama Ibu")]
        public string MotherName { get; set; } //(varchar(100), null)

        [Display(Name = "Status Baptis/Sidi Ibu")]
        public string MotherChrismationType { get; set; } //(varchar(1), null)

        [Display(Name = "No. Anggota Ibu")]
        public string MotherMemberNo { get; set; } //(varchar(20), null)

        [Display(Name = "Etnis")]
        public int? EthnicId { get; set; } //(int, null)

        [Display(Name = "Alasan At. Masuk")]
        public string JoinNote { get; set; } //(varchar(500), null)

        [Display(Name = "Catatan At. Keluar")]
        public string ResignNote { get; set; } //(varchar(500), null)

        [Display(Name = "Gereja Baptis Anak")]
        public int? ChildhoodBaptizedShrineId { get; set; } //(int, null)

        [Display(Name = "Yang Melayani Baptis Anak")]
        public string ChildhoodBaptizedPriest { get; set; } //(varchar(100), null)

        [Display(Name = "Tanggal Baptis Anak")]
        public DateTime? ChildhoodBaptizedDate { get; set; } //(date, null)

        [Display(Name = "Nomor Akte Baptis Anak")]
        public string ChildhoodBaptizedCert { get; set; } //(varchar(100), null)

        [Display(Name = "Flag ATIOT")]
        public bool ChildhoodBaptizedFlagATIOT { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; } //(varchar(50), null)        
        public DateTime ChangedWhen { get; set; } //(datetime, not null)
        public string ChangedBy { get; set; } //(varchar(50), not null

        public IEnumerable<MemberMarriageViewModel> Marriages { get; set; }
        public IEnumerable<ListMemberStateHistoryViewModel> StateHistories { get; set; }
        public IEnumerable<MemberNoteViewModel> Notes { get; set; }

        public IEnumerable<ListMemberVisitViewModel> Visits { get; set; }

        [Display(Name = "Tanggal Kematian")]
        public DateTime? DeceasedDate { get; set; }

        [Display(Name = "Catatan Kematian")]
        public string DeceasedNotes { get; set; }
    }
}
