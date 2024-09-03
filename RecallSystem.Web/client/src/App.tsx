import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Recall, ExecucaoRecall } from './interfaces/types';

//dev
const API_BASE_URL = 'https://localhost:7119/api';

//prod
// const API_BASE_URL = '/api'; 

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
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-4">Sistema de Recalls</h1>

      <div className="mb-4">
        <h2 className="text-xl font-semibold mb-2">Lista de Recalls</h2>
        <ul className="list-disc pl-5">
          {recalls.map((recall) => (
            <li key={recall.id}>
              {recall.titulo} - {new Date(recall.dataPublicacao).toLocaleDateString()} - {recall.descricao}
            </li>
          ))}
        </ul>
      </div>

      <div className="mb-4">
        <h2 className="text-xl font-semibold mb-2">Pesquisa por Chassi</h2>
        <div className="flex">
          <input
            type="text"
            value={chassi}
            onChange={(e: any) => setChassi(e.target.value)}
            placeholder="Digite o chassi"
            className="border p-2 mr-2"
          />
          <button onClick={handleChassiSearch} className="bg-blue-500 text-white px-4 py-2 rounded">
            Pesquisar
          </button>
        </div>
      </div>

      {error && <p className="text-red-500">{error}</p>}

      {execucoes.length > 0 && (
        <div>
          <h2 className="text-xl font-semibold mb-2">Resultados da Pesquisa</h2>
          <ul className="list-disc pl-5">
            {execucoes.map((execucao) => (
              <li key={execucao.id}>
                {execucao.recall.titulo}
                {execucao.dataExecucao && (
                  <span> - Executado em {new Date(execucao.dataExecucao).toLocaleDateString()}</span>
                )}
                {execucao.concessionaria && <span> por {execucao.concessionaria}</span>}
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
};

export default App;