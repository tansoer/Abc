using Abc.Core.Loinc.Response;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Abc.Core.Loinc.Infra
{
    public interface ILoincRepo {
        Task<List<LoincResponse>> GetAsync();
        List<LoincResponse> Get();
        Task<LoincResponse> GetAsync(string id);
        Task<List<LoincResponse>> GetTerms(string termName);
        Task<List<LoincResponse>> GetTermsByRule(string termName);
        Task<List<LoincResponse>> FilterByCompositeString(string composite);
        Task<List<LoincResponse>> FilterByExpression(Expression expression);
        LoincResponse Get(string id);
    }
}