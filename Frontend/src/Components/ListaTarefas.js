import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ListaTarefas() {
  const [tarefas, setTarefas] = useState([]);

  useEffect(() => {
    // Substitua pela URL da sua API
    axios.get('http://localhost:5000/api/tarefas') 
      .then(response => {
        setTarefas(response.data);
      })
      .catch(error => {
        console.error('Houve um erro ao buscar as tarefas!', error);
      });
  }, []);

  return (
    <div>
      <h2>Lista de Tarefas</h2>
      <ul>
        {tarefas.map(tarefa => (
          <li key={tarefa.id}>
            <strong>{tarefa.titulo}</strong> - {tarefa.status}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ListaTarefas;
