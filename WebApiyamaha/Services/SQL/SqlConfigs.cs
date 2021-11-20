using System.Collections.Generic;

namespace Toyota.BLL.Services.SqlServise
{
    public class SqlConfigs
    {


        public Dictionary<int, string> SqlQuery { get; private set; }


        public SqlConfigs(string id)
        {
            SqlQuery = new()
            {
                [0] = $"select bt.Id, bt.[label] from NewBodyTypes as bt inner join NewGenerations as gen on bt.ProjectIdGen = gen.projectId where gen.Id = {id}",
                [1] = $"select eng.Id, eng.[label] from NewEngines as eng inner join NewGenerations as gen on eng.ProductIdGen = gen.projectId where gen.Id = {id}",
                [2] = $"select t.Id, t.[label] from Transmissions as t inner join NewGenerations as gen on t.ProjectIdGen = gen.projectId where gen.Id = {id}", //Перенаправить на новую таблицу, как будут исправлены нюансы парсинга
                [3] = $"select st.Id, st.[label] from NewSteerings as st inner join NewGenerations as gen on st.ProjectIdGen = gen.projectId where gen.Id = {id}",
                [4] = $"select pg.Id, pg.factoryGrade from NewProductionGrades as pg inner join NewGenerations as gen on pg.projectIdGen = gen.ProjectId where gen.Id = {id}",
                [5] = $"select pp.Id, pp.[label] from NewProductionPlants as pp inner join NewGenerations as gen on pp.projectIdGen = gen.ProjectId where gen.Id = {id}"
            };
        }
    }

    public enum Table
    {
        Categories,
        Engines,
        Transmissions,
        Steerings,
        ProductionGrades,
        ProductionPlants
    }
}
