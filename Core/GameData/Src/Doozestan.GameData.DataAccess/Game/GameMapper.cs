using System.Data;
using AutoMapper;
using Framework.Mapper;

namespace Doozestan.GameData.Dao
{
  public  class GameMapper : BaseMapper<Domain.Game>
  {
      public override Domain.Game InnerMapRow(IDataRecord reader)
      {
            var mapped = Mapper.DynamicMap<Domain.Game>(reader);
            return mapped;
        }
  }
}
