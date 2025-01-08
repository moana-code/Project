import React, { useState } from 'react';
import axios from 'axios';
import { useHistory } from 'react-router-dom';

function CriarTarefa() {
  const [titulo, setTitulo] = useState('');
  const [descricao, setDescricao] = useState('');
  const [prioridade, setPrioridade] = useState('');
  const [status, setStatus] = useState('');
  
  const history = useHistory();

  const handleSubmit = (e) => {
    e.preventDefault();

    const tarefa = { titulo, descricao, prioridade, status };

    axios.post('http://localhost:5000/api/tarefas', tarefa)
      .then(response => {
        alert('Tarefa criada com sucesso!');
        history.push('/');
      })
      .catch(error => {
        console.error('Houve um erro ao criar a tarefa!', error);
        alert('Erro ao criar tarefa');
      });
  };

  return (
    <div>
      <h2>Criar Tarefa</h2>
      <form onSubmit={handleSubmit}>
        <input 
          type="text" 
          placeholder="Título" 
          value={titulo}
          onChange={(e) => setTitulo(e.target.value)}
        />
        <textarea 
          placeholder="Descrição" 
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
        />
        <input 
          type="text" 
          placeholder="Prioridade" 
          value={prioridade}
          onChange={(e) => setPrioridade(e.target.value)}
        />
        <input 
          type="text" 
          placeholder="Status" 
          value={status}
          onChange={(e) => setStatus(e.target.value)}
        />
        <button type="submit">Criar Tarefa</button>
      </form>
    </div>
  );
}

export default CriarTarefa;
