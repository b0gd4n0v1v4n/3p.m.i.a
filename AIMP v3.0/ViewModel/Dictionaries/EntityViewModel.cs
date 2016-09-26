using System.Collections.Generic;

namespace AIMP_v3._0.ViewModel.Dictionaries
{
    public class EntityViewModel
    {
        public int Id { get;}
        public string Name { get; }
        public IEnumerable<CellViewModel> Cells { get; }
        public EntityViewModel(string name, IEnumerable<CellViewModel> cells, int? id = null )
        {
            Id = !id.HasValue ? 0 : id.Value;
            Name = name;
            Cells = cells;
        }
    }
}
