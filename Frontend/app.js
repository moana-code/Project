const API_URL = "http://localhost:5000/api/usuario";

// Função para obter usuários
async function obterUsuarios() {
    try {
        const response = await fetch(API_URL);
        const usuarios = await response.json();
        console.log(usuarios);
    } catch (error) {
        console.error("Erro ao obter usuários:", error);
    }
}

// Função para criar usuário
async function criarUsuario(usuario) {
    try {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(usuario),
        });

        if (!response.ok) throw new Error("Erro ao criar usuário");
        console.log("Usuário criado com sucesso!");
    } catch (error) {
        console.error(error);
    }
}

// Exemplo de uso
obterUsuarios();

criarUsuario({
    nome: "João",
    email: "joao@email.com",
    senha: "senha123",
});
