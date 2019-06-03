using Dapper.FastCrud;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers
{
    public class New
    {
        [Key]
        [DatabaseGeneratedDefaultValue]
        public int Key { get; set; }
        [ForeignKey("World")]
        public int? WorldId { get; set; }
        public World World { get; set; }

    }
}