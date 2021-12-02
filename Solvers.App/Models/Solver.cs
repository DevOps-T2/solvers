using ProtoBuf;

namespace Solvers.App.Models
{
    [ProtoContract]
    public class Solver
    {
        [ProtoMember(1)]
        public long Id { get; set; }
        
        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public string Image { get; set; }
    }
}
