import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Recall, ExecucaoRecall } from './interfaces/types';

//dev
// const API_BASE_URL = 'https://localhost:7119/api';

//prod
const API_BASE_URL = '/api'; 

function App() {
  const [recalls, setRecalls] = useState<Recall[]>([]);
  const [chassi, setChassi] = useState<string>('');
  const [execucoes, setExecucoes] = useState<ExecucaoRecall[]>([]);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    fetchRecalls();
  }, []);

  const fetchRecalls = async (): Promise<void> => {
    try {
      const response = await axios.get<Recall[]>(`${API_BASE_URL}/recall`);
      setRecalls(response.data);
    } catch (error) {
      console.error('Erro ao buscar recalls:', error);
    }
  };

  const handleChassiSearch = async (): Promise<void> => {
    try {
      const response = await axios.get<ExecucaoRecall[]>(`${API_BASE_URL}/recall/chassi/${chassi}`);
      setExecucoes(response.data);
      setError('');
    } catch (error) {
      console.error('Erro ao buscar recalls por chassi:', error);
      setExecucoes([]);
      setError('Nenhum recall encontrado para o chassi informado.');
    }
  };

  return (
    <div className="container">
      <h1>Sistema de Recalls</h1>

      <div className="mb-4">
        <h2>Lista de Recalls</h2>
        <table className="table-auto w-full border border-gray-400">
          <thead>
            <tr>
              <th className="border border-gray-400 p-2">Título</th>
              <th className="border border-gray-400 p-2">Data de Publicação</th>
              <th className="border border-gray-400 p-2">Descrição</th>
            </tr>
          </thead>
          <tbody>
            {recalls.map((recall) => (
              <tr key={recall.id}>
                <td className="border border-gray-400 p-2">{recall.titulo}</td>
                <td className="border border-gray-400 p-2">{new Date(recall.dataPublicacao).toLocaleDateString('pt-BR')}</td>
                <td className="border border-gray-400 p-2">{recall.descricao}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="mb-4">
        <h2>Pesquisa por Chassi</h2>
        <div className="flex">
          <input
            type="text"
            value={chassi}
            onChange={(e: any) => setChassi(e.target.value)}
            placeholder="Digite o chassi"
          />
          <button onClick={handleChassiSearch}>
            Pesquisar
          </button>
        </div>
      </div>

      {error && <p className="text-red-500">{error}</p>}

      {execucoes.length > 0 && (
        <div>
          <h2>Resultados da Pesquisa</h2>
          <table className="table-auto w-full border border-gray-400">
            <thead>
              <tr>
                <th className="border border-gray-400 p-2">Título do Recall</th>
                <th className="border border-gray-400 p-2">Data de Execução</th>
                <th className="border border-gray-400 p-2">Concessionária</th>
              </tr>
            </thead>
            <tbody>
              {execucoes.map((execucao) => (
                <tr key={execucao.id}>
                  <td className="border border-gray-400 p-2">{execucao.recall.titulo}</td>
                  <td className="border border-gray-400 p-2">
                    {execucao.dataExecucao ? new Date(execucao.dataExecucao).toLocaleDateString('pt-BR') : 'Não Executado'}
                  </td>
                  <td className="border border-gray-400 p-2">{execucao.concessionaria || 'N/A'}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

export default App;
