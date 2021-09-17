using Npgsql;
using System;
using System.Data;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Repositories
{
    public class DapperPostgreContext : IDbContext, IDisposable
    {
        private NpgsqlConnection _conexao = null;

        /// <summary>
        /// A string de conexão.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="userId">O identificador do usuário.</param>
        /// <param name="applicationName">O nome da aplicação.</param>
        public DapperPostgreContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Obtém a conexão com a base de dados para o contexto.
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return GetConnection();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_conexao?.State == ConnectionState.Open)
            {
                _conexao.Close();
            }

            _conexao?.Dispose();
            _conexao = null;
        }

        public IDbConnection GetConnection()
        {
            if (_conexao == null)
            {
                var conexaoPg = new NpgsqlConnection(this.connectionString);

                _conexao = conexaoPg;
            }

            if (_conexao.State != ConnectionState.Open)
            {
                _conexao.Open();
            }

            return _conexao;
        }
    }
}