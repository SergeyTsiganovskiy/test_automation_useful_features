using System.Data;
using System.Linq;

namespace test_automation_useful_features.Helpers.Examples.DB
{
    public class ProjectRepo
    {
        private readonly string connectionString;
        private readonly string clientName;

        public ProjectRepo(string connectionString, string clientName)
        {
            this.connectionString = connectionString;
            this.clientName = clientName;
        }

        /// <summary>
        /// Method to delete projects
        /// </summary>
        /// <param name="projectId">Project id to delete</param>
        public void DeleteProjectById(string projectId)
        {
            LogUtil.log.Info($"{System.Reflection.MethodBase.GetCurrentMethod()} => Delete project from rfmc_invoices_{clientName} database. Project_id = \"{projectId}\".");
            using (PostgreSqlDbClient dbcMcInvoices = new PostgreSqlDbClient(connectionString, $"rfmc_invoices_{clientName}"))
            {
                dbcMcInvoices.ExecuteModifyQuery(GetQueryToDeleteProjectFromMCInvoices(projectId));
            }

            LogUtil.log.Info($"{System.Reflection.MethodBase.GetCurrentMethod()} => Delete project from rfmc_validations_{clientName} database. Project_id = \"{projectId}\".");
            using (PostgreSqlDbClient dbcMcValidations = new PostgreSqlDbClient(connectionString, $"rfmc_validations_{clientName}"))
            {
                dbcMcValidations.ExecuteModifyQuery(GetQueryToDeleteProjectFromMCValidations(projectId));
            }

            LogUtil.log.Info($"{System.Reflection.MethodBase.GetCurrentMethod()} => Delete project from common_methodologyoutputs_{clientName} database. Project_id = \"{projectId}\".");
            using (PostgreSqlDbClient dbcMcOutputs = new PostgreSqlDbClient(connectionString, $"common_methodologyoutputs_{clientName}"))
            {
                DataTable outputsRegistryTableContent = dbcMcOutputs.ExecuteSelectQuery(GetQueryToReceiveOutputTableName(projectId));
                string outputsTableName = outputsRegistryTableContent.Rows.Cast<DataRow>().FirstOrDefault()?["output_table_name"].ToString();
                if (outputsTableName != null)
                {
                    var t = dbcMcOutputs.ExecuteModifyQuery(GetQueryToDropOutputsTable(outputsTableName));
                }
            }
        }
        
        #region Queries

        private string GetQueryToDeleteProjectFromMCInvoices(string projectId)
        {
            return $@"
            delete from rebate_summary where project_id = '{projectId}';
            delete from invoice_scripts where invoice_id = '{projectId}';
            delete from invoice_operation_log where invoice_id = '{projectId}';
            delete from invoice_load where invoice_id = '{projectId}';
            delete from submissions where invoice_id = '{projectId}';
            delete from invoice where invoice_id = '{projectId}';";
        }

        private string GetQueryToDeleteProjectFromMCValidations(string projectId)
        {
            return $@"
            delete from submissions where invoice_id = '{projectId}';
            delete from invoice where invoice_id = '{projectId}';";
        }

        private string GetQueryToReceiveOutputTableName(string projectId)
        {
            return $@"
            select output_table_name from outputs_registry where context_id = '{projectId}'";
        }

        private string GetQueryToDropOutputsTable(string outputTableName)
        {
            return $@"drop table {outputTableName}";
        }
        #endregion
    }
}
