import React, { useState, useEffect } from 'react';
import api from './api';

function App() {
  const [tarefas, setTarefas] = useState([]);
  const [status, setStatus] = useState("Pendente"); // Estado para filtrar tarefas

  // UseEffect para buscar as tarefas na API
  useEffect(() => {
    // Função para buscar tarefas por status
    const fetchTarefas = async () => {
      try {
        const response = await api.get(`/tarefas/PorStatus/${status}`);
        setTarefas(response.data); // Salva as tarefas no estado
      } catch (error) {
        console.error('Erro ao carregar tarefas:', error);
      }
    };
    fetchTarefas(); // Chama a função para obter as tarefas
  }, [status]); // Sempre que o status mudar, irá refazer a requisição

  return (
    <div>
      <h1>Gestor de Tarefas</h1>
      
      {/* Seletor de Status para filtrar tarefas */}
      <label>
        Status:
        <select onChange={e => setStatus(e.target.value)} value={status}>
          <option value="Pendente">Pendente</option>
          <option value="Concluída">Concluída</option>
          <option value="Em Progresso">Em Progresso</option>
        </select>
      </label>

      {/* Exibir as tarefas */}
      <ul>
        {tarefas.length === 0 ? (
          <li>Nenhuma tarefa encontrada para o status selecionado.</li>
        ) : (
          tarefas.map(tarefa => (
            <li key={tarefa.id}>
              <h3>{tarefa.titulo}</h3>
              <p><strong>Status:</strong> {tarefa.status}</p>
              <p><strong>Prioridade:</strong> {tarefa.prioridade}</p>
            </li>
          ))
        )}
      </ul>
    </div>
  );
}

export default App;
import './App.css';

