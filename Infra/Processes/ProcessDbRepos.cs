using Abc.Domain.Processes;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Processes {
    public static class ProcessDbRepos {
        public static void Register(IServiceCollection c) {
            c.AddTransient<IProcessTypesRepo, ProcessTypesRepo>();
            c.AddTransient<IThreadsRepo, ThreadsRepo>();
            c.AddTransient<IAttributeValuesRepo, AttributeValuesRepo>();
            c.AddTransient<IActionsRepo, ActionsRepo>();
            c.AddTransient<IActionTypesRepo, ActionTypesRepo>();
            c.AddTransient<IActionApproversRepo, ActionApproversRepo>();
            c.AddTransient<IAttributeValuesRepo, AttributeValuesRepo>();
            c.AddTransient<IOutcomeTypesRepo, OutcomeTypesRepo>();
            c.AddTransient<IOutcomesRepo, OutcomesRepo>();
            c.AddTransient<IOutcomeApproversRepo, OutcomeApproversRepo>();
            c.AddTransient<ITasksRepo, TasksRepo>();
            c.AddTransient<ITaskTypesRepo, TaskTypesRepo>();
            c.AddTransient<IAttributeTypesRepo, AttributeTypesRepo>();
            c.AddTransient<IThreadTypesRepo, ThreadTypesRepo>();
            c.AddTransient<IProcessesRepo, ProcessesRepo>();
            c.AddTransient<ITaskParticipantsRepo, TaskParticipantsRepo>();

        }
    }
}