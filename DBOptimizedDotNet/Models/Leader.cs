using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBOptimizedDotNet.Models
{
   

    public class Leader : ICloneable
    {


        public Leader(string name, string nameWithCode, string mobileNo, string phone, string email, string address, string photoUrl, string coverPhotoUrl, string description, string autoGenCode, string genderName, string bloodGroupName, string activationStatusName, string freelancingAdjustHistory, string rejectedReason, string placementTeamOrder, string ownershipChangeHistory, string referralChangeHistory, string placementChangeHistory, string password, string publicIpAddress, string localIpAddress, string macAddress, string browserInfo, string disableReason, string enableReason, bool isUserChosen, bool isCalculated, bool isMaskEmail, bool isOrgAcc, bool isDisabled, bool isDeleted, int? genderId, int? bloodGroupId, int? districtId, int? policeStationId, int? unionId, long? statusId, int activationStatusId, long? createdLeaderId, long? referralId, long? placementId, long? parentId, long? firstGenId, long? userId, long? rankId, double unit, double eCommerceUnit, double freelancingAmount, double balance, int totalAssociate, int teamATotalAssociate, int teamBTotalAssociate, int teamCTotalAssociate, int teamDTotalAssociate, int teamETotalAssociate, int teamFTotalAssociate, long organizationId, long? lastModifiedById, long createdById, long? disabledById, long? enabledById, long? deletedById, DateTime? dateOfBirth, DateTime joiningDate, DateTime? approvedOn, DateTime? rejectedOn, DateTime createdOn, DateTime? lastModifiedOn, DateTime? disabledOn, DateTime? disabledFrom, DateTime? disabledTo, DateTime? enabledOn, DateTime? deletedOn)
        {
          
            Name = name;
            NameWithCode = nameWithCode;
            MobileNo = mobileNo;
            Phone = phone;
            Email = email;
            Address = address;
            PhotoUrl = photoUrl;
            CoverPhotoUrl = coverPhotoUrl;
            Description = description;
            AutoGenCode = autoGenCode;
            GenderName = genderName;
            BloodGroupName = bloodGroupName;
            ActivationStatusName = activationStatusName;
            FreelancingAdjustHistory = freelancingAdjustHistory;
            RejectedReason = rejectedReason;
            PlacementTeamOrder = placementTeamOrder;
            OwnershipChangeHistory = ownershipChangeHistory;
            ReferralChangeHistory = referralChangeHistory;
            PlacementChangeHistory = placementChangeHistory;
            Password = password;
            PublicIpAddress = publicIpAddress;
            LocalIpAddress = localIpAddress;
            MacAddress = macAddress;
            BrowserInfo = browserInfo;
            DisableReason = disableReason;
            EnableReason = enableReason;
            IsUserChosen = isUserChosen;
            IsCalculated = isCalculated;
            IsMaskEmail = isMaskEmail;
            IsOrgAcc = isOrgAcc;
            IsDisabled = isDisabled;
            IsDeleted = isDeleted;
            GenderId = genderId;
            BloodGroupId = bloodGroupId;
            DistrictId = districtId;
            PoliceStationId = policeStationId;
            UnionId = unionId;
            StatusId = statusId;
            ActivationStatusId = activationStatusId;
            CreatedLeaderId = createdLeaderId;
            ReferralId = referralId;
            PlacementId = placementId;
            ParentId = parentId;
            FirstGenId = firstGenId;
            UserId = userId;
            RankId = rankId;
            Unit = unit;
            ECommerceUnit = eCommerceUnit;
            FreelancingAmount = freelancingAmount;
            Balance = balance;
            TotalAssociate = totalAssociate;
            TeamATotalAssociate = teamATotalAssociate;
            TeamBTotalAssociate = teamBTotalAssociate;
            TeamCTotalAssociate = teamCTotalAssociate;
            TeamDTotalAssociate = teamDTotalAssociate;
            TeamETotalAssociate = teamETotalAssociate;
            TeamFTotalAssociate = teamFTotalAssociate;
            OrganizationId = organizationId;
            LastModifiedById = lastModifiedById;
            CreatedById = createdById;
            DisabledById = disabledById;
            EnabledById = enabledById;
            DeletedById = deletedById;
            DateOfBirth = dateOfBirth;
            JoiningDate = joiningDate;
            ApprovedOn = approvedOn;
            RejectedOn = rejectedOn;
            CreatedOn = createdOn;
            LastModifiedOn = lastModifiedOn;
            DisabledOn = disabledOn;
            DisabledFrom = disabledFrom;
            DisabledTo = disabledTo;
            EnabledOn = enabledOn;
            DeletedOn = deletedOn;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid? Guid { get; set; }


        public string Name { get; set; }
        public string NameWithCode { get; set; }
        public string MobileNo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string Description { get; set; }
        public string AutoGenCode { get; set; }
        public string GenderName { get; set; }

        public string BloodGroupName { get; set; }
        public string ActivationStatusName { get; set; }
        public string FreelancingAdjustHistory { get; set; }



        public string RejectedReason { get; set; }
        public string PlacementTeamOrder { get; set; }
        public string OwnershipChangeHistory { get; set; } //500
        public string ReferralChangeHistory { get; set; } //500
        public string PlacementChangeHistory { get; set; } //500

        public string Password { get; set; } // Temporary
        public string PublicIpAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string MacAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string DisableReason { get; set; }
        public string EnableReason { get; set; }


        public bool IsUserChosen { get; set; }
        public bool IsCalculated { get; set; }
        public bool IsMaskEmail { get; set; }
        public bool IsOrgAcc { get; set; }

        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }


        public int? GenderId { get; set; }
      
        public int? BloodGroupId { get; set; }
 

        public int? DistrictId { get; set; }
        public int? PoliceStationId { get; set; }
        public int? UnionId { get; set; }


     
       

        public long? StatusId { get; set; } // if no Status. then it show "Basic"
        public int ActivationStatusId { get; set; } 
     

        public long? CreatedLeaderId { get; set; } 
        public long? ReferralId { get; set; } 
        public long? PlacementId { get; set; } 
        public long? ParentId { get; set; } 
        public long? FirstGenId { get; set; } 
        public long? UserId { get; set; }


        public long? RankId { get; set; }


        public double Unit { get; set; }
        public double ECommerceUnit { get; set; }
        public double FreelancingAmount { get; set; }

        public double Balance { get; set; }

        public int TotalAssociate { get; set; }





        public int TeamATotalAssociate { get; set; }
        public int TeamBTotalAssociate { get; set; }

        public int TeamCTotalAssociate { get; set; }


        public int TeamDTotalAssociate { get; set; }
        public int TeamETotalAssociate { get; set; }
        public int TeamFTotalAssociate { get; set; }




        public long OrganizationId { get; set; }




        public long? LastModifiedById { get; set; }
        public long CreatedById { get; set; }


        public long? DisabledById { get; set; }

        public long? EnabledById { get; set; }

        public long? DeletedById { get; set; }




        public DateTime? DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }  



        public DateTime? ApprovedOn { get; set; }
        public DateTime? RejectedOn { get; set; }
         public DateTime CreatedOn { get; set; }
 
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DisabledOn { get; set; }
        public DateTime? DisabledFrom { get; set; }
        public DateTime? DisabledTo { get; set; }

        public DateTime? EnabledOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
 