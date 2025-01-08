import React, { useState } from 'react';
import axios from 'axios';
import { useHistory } from 'react-router-dom';

function Login() {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const history = useHistory();

  const handleLogin = (e) => {
    e.preventDefault();

    const usuario = { email, senha };

    axios.post('http://localhost:5000/api/usuarios/login', usuario)
      .then(response => {
        alert('Login realizado com sucesso!');
        localStorage.setItem('token', response.data.token); // Armazena o token no localStorage
        history.push('/'); // Redireciona para a lista de tarefas
      })
      .catch(error => {
        console.error('Erro de login!', error);
        alert('Usuário ou senha inválidos!');
      });
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleLogin}>
        <input 
          type="email" 
          placeholder="E-mail" 
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input 
          type="password" 
          placeholder="Senha"
          value={senha}
          onChange={(e) => setSenha(e.target.value)}
        />
        <button type="submit">Login</button>
      </form>
    </div>
  );
}

export default Login;
