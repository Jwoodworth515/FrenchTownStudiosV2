using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FrenchTownStudiosV2.DATA.EF/*.DBFrenchTownStudiosV2Metadata*/
{
        #region ClientAssetMetadata

        public class ClientAssetMetadata
        {
            //public int ClientAssetsId { get; set; }

            [Required(ErrorMessage = "Asset Name is required")]
            [Display(Name = "Asset Name")]
            [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
            public string AssetName { get; set; }

            [Required(ErrorMessage = "Client Id is required")]
            [Display(Name = "Client Id")]
            [StringLength(128, ErrorMessage = "Value must be 128 characters or less")]
            public string ClientId { get; set; }

            [Display(Name = "Client Photo")]
            [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
            public string AssetPhoto { get; set; }

            [Display(Name = "Notes")]
            [StringLength(300, ErrorMessage = "Value must be 300 characters or less")]
            [UIHint("MultilineText")]
            public string SpecialNotes { get; set; }

            [Required(ErrorMessage = "Active Status is required")]
            [Display(Name = "Active Status")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = "Date Added is required")]
            [Display(Name = "Date Added")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public System.DateTime DateAdded { get; set; }

            [MetadataType(typeof(ClientAssetMetadata))]
            public partial class ClientAsset
            {

            }

        }

        #endregion

        #region ClientDetailMetadata

        public class ClientDetailMetadata
        {
            //public string ClientId { get; set; }

            [Required(ErrorMessage = "First Name is required")]
            [Display(Name = "First Name")]
            [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last Name is required")]
            [Display(Name = "Last Name")]
            [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
            public string LastName { get; set; }

            [MetadataType(typeof(ClientDetailMetadata))]
            public partial class ClientDetail
            {

            }
        }

        #endregion

        #region LocationMetadata

        public class LocationMetadata
        {
            //public int LocationId { get; set; }

            [Required(ErrorMessage = "Location Name is required")]
            [Display(Name = "Location Name")]
            [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
            public string LocationName { get; set; }

            [Required(ErrorMessage = "Address is required")]
            [StringLength(100, ErrorMessage = "Value must be 100 characters or less")]
            public string Address { get; set; }

            [Required(ErrorMessage = "City is required")]
            [StringLength(100, ErrorMessage = "Value must be 100 characters or less")]
            public string City { get; set; }

            [Required(ErrorMessage = "State is required")]
            [StringLength(2, ErrorMessage = "Value must be 2 characters or less")]
            public string State { get; set; }

            [Required(ErrorMessage = "Zip Code is required")]
            [Display(Name = "Zip")]
            [StringLength(5, ErrorMessage = "Value must be 5 characters or less")]
            public string ZipCode { get; set; }

            [Required(ErrorMessage = "Reservation Limit is required")]
            [Display(Name = "Reservation Limit")]
            public byte ReservationLimit { get; set; }

            [MetadataType(typeof(LocationMetadata))]
            public partial class Location
            {

            }

        }

        #endregion

        #region ReservationMetadata

        public class ReservationMetadata
        {
            //public int ReservationId { get; set; }

            [Required(ErrorMessage = "Client Assets Id is required")]
            [Display(Name = "Client Assets Id")]
            public int ClientAssetsId { get; set; }

            [Required(ErrorMessage = "Location Id is required")]
            [Display(Name = "Location Id")]
            public int LocationId { get; set; }

            [Required(ErrorMessage = "Reservation Date is required")]
            [Display(Name = "Reservation Date")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public System.DateTime ReservationDate { get; set; }

            [MetadataType(typeof(ReservationMetadata))]
            public partial class Reservation
            {

            }
        }

        #endregion

}
