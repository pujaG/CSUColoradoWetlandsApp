﻿using PCLStorage;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PortableApp.Models
{
    [Table("wetland_plants")]
    public class WetlandPlant
    {
        [PrimaryKey]
        public int plantid { get; set; }

        [Unique]
        public int id { get; set; }
        public string sections { get; set; }
        public string scinameauthor { get; set; }
        public string scinamenoauthor { get; set; }
        public string family { get; set; }
        public string commonname { get; set; }
        public string plantscode { get; set; }
        public string mapimg { get; set; }
        public string itiscode { get; set; }
        public string awwetcode { get; set; }
        public string gpwetcode { get; set; }
        public string wmvcwetcode { get; set; }
        public string cvalue { get; set; }
        public string grank { get; set; }
        public string federalstatus { get; set; }
        public string cosrank { get; set; }
        public string mtsrank { get; set; }
        public string ndsrank { get; set; }
        public string sdsrank { get; set; }
        public string utsrank { get; set; }
        public string wysrank { get; set; }
        public string nativity { get; set; }
        public string noxiousweed { get; set; }
        public int elevminfeet { get; set; }
        public int elevminm { get; set; }
        public int elevmaxfeet { get; set; }
        public int elevmaxm { get; set; }
        public string keychar1 { get; set; }
        public string keychar2 { get; set; }
        public string keychar3 { get; set; }
        public string keychar4 { get; set; }
        public string keychar5 { get; set; }
        public string keychar6 { get; set; }
        public string similarsp { get; set; }
        public string habitat { get; set; }
        public string comments { get; set; }
        public string animaluse { get; set; }
        public string ecologicalsystems { get; set; }
        public string synonyms { get; set; }
        public string topimgtopimg { get; set; }
        public string duration { get; set; }
        public string color { get; set; }
        public string leafdivision { get; set; }
        public string leafshape { get; set; }
        public string leafarrangement { get; set; }
        public string plantsize { get; set; }
        //public string FruitWetland { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantImage> Images { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantReference> References { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantSimilarSpecies> SimilarSpecies { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantFruits> FruitWetland { get; set; }
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantSize> SizeWetland { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantShape> ShapeWetland { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantArrangement> ArrangementWetland { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandPlantDivision> DivisionWetland { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandCountyPlant> CountyPlantWetland { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WetlandRegions> RegionWetland { get; set; }


        public IFolder rootFolder { get { return FileSystem.Current.LocalStorage; } }

        public string ThumbnailPathDownloaded { get { return rootFolder.Path + "/Images/" + plantscode + "_icon.jpg"; } }
        public string ThumbnailPathStreamed { get {

                if (id == 710)
                {

                    return "http://sdt1.agsci.colostate.edu/mobileapi/api/wetland/image_icons/ASCMIO";
                }

                return "http://sdt1.agsci.colostate.edu/mobileapi/api/wetland/image_icons/" + plantscode;

            } }



        public string RangePathDownloaded {
            get {
                return rootFolder.Path + "/Images/" + mapimg;
            }
        }

        public string RangePathStreamed
        {
            get {
                return "http://sdt1.agsci.colostate.edu/mobileapi/api/wetland/range_images/" + mapimg.Replace(".png", "");
            }

        }
        
        public string scinameauthorstripped
        {
            get { return scinameauthor.Replace("<em>", "").Replace("</em>", ""); }
        }

        public string scinamenoauthorstripped
        {
            get { return scinamenoauthor.Replace("<em>", "").Replace("</em>", ""); }
        }

        public bool isFavorite { get; set; }

        public string scinamenoauthorFirstInitial { get { return scinamenoauthorstripped[0].ToString(); } }
        public string commonnameFirstInitial { get { return commonname[0].ToString(); } }
        public string familyFirstInitial { get { return family[0].ToString(); } }
        public string sectionsFirstInitial { get { return sections[0].ToString(); } }

    }

}
