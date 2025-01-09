import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Switch, Link } from 'react-router-dom';
import api from './api';
import CriarTarefa from './CriarTarefa';

function App() {
  const [tarefas, setTarefas] = useState([]);
  const [status, setStatus] = useState("Pendente");

  useEffect(() => {
    const fetchTarefas = async () => {
      try {
        const response = await api.get(`/tarefas/PorStatus/${status}`);
        setTarefas(response.data);
      } catch (error) {
        console.error('Erro ao carregar tarefas:', error);
      }
    };
    fetchTarefas();
  }, [status]);

  return (
    <Router>
      <div>
        <h1>Gestor de Tarefas</h1>

        {/* Navegação */}
        <nav>
          <ul>
            <li><Link to="/">Home</Link></li>
            <li><Link to="/criar-tarefa">Criar Tarefa</Link></li>
          </ul>
        </nav>

        {/* Definição de Rotas */}
        <Switch>
          <Route path="/" exact>
            <div>
              <label>
                Status:
                <select onChange={e => setStatus(e.target.value)} value={status}>
                  <option value="Pendente">Pendente</option>
                  <option value="Concluída">Concluída</option>
                  <option value="Em Progresso">Em Progresso</option>
                </select>
              </label>

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
          </Route>
          <Route path="/criar-tarefa" component={CriarTarefa} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
