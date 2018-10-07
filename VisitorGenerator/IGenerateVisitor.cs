using System.Threading.Tasks;

namespace CarWash.VisitorGenerator
{
    /// <summary>
    /// Visitor Generator Interface
    /// </summary>
    interface IGenerateVisitor
    {
        /// <summary>
        /// Starts the generation.
        /// </summary>
        /// <returns></returns>
        Task StartGeneration();
    }
}