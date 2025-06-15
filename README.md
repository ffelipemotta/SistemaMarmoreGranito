# Sistema Mamore Granito

> Sistema de gerenciamento de estoque para marmoraria, desenvolvido em ASP.NET Core MVC.

## Integrantes do projeto: 

- THALES MENINI FERRARI
- TIAGO CEZAR SILVA E SILVA
- FELIPE MOTTA NEVES 
- FILIPE ANTÔNIO LINS NOÉ



O Sistema Mamore Granito é uma solução completa para gerenciamento de marmorarias, permitindo o controle de estoque de blocos e chapas de mármore e granito, além de gerenciar todo o processo de serragem. O sistema foi desenvolvido para otimizar o controle de estoque e melhorar a eficiência operacional das marmorarias.

---

## 📋 Índice

- [Sobre](#sobre)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Como Começar](#como-começar)
  - [Pré-requisitos](#pré-requisitos)
  - [Instalação](#instalação)
- [Como Usar](#como-usar)
- [Como Contribuir](#como-contribuir)
- [Licença](#licença)
- [Contato](#contato)

---

## 🧐 Sobre

O Sistema Mamore Granito nasceu da necessidade de modernizar e digitalizar o controle de estoque em marmorarias. O projeto visa resolver os principais desafios enfrentados por empresas do setor:

- Controle manual de estoque propenso a erros
- Dificuldade no rastreamento de blocos e chapas
- Falta de visibilidade sobre o processo de serragem
- Gestão ineficiente do inventário

---

## ✨ Funcionalidades

- **Gerenciamento de Blocos:**
  - Cadastro completo de blocos com dimensões e características
  - Controle de disponibilidade
  - Histórico de movimentações
  - Cálculo automático de peso

- **Controle de Chapas:**
  - Registro detalhado de chapas serradas
  - Acompanhamento de dimensões e espessuras
  - Controle de estoque por tipo de material
  - Cálculo de valor por metro quadrado

- **Processos de Serragem:**
  - Registro completo do processo de serragem
  - Cálculo automático de quantidade de chapas
  - Controle de eficiência do processo
  - Histórico de serragens por bloco

- **Dashboard Intuitivo:**
  - Visão geral do estoque
  - Estatísticas em tempo real
  - Indicadores de performance
  - Interface moderna e responsiva

---

## 🛠️ Tecnologias Utilizadas

- **Backend:**
  - [ASP.NET Core MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)
  - [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)
  - [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)

- **Frontend:**
  - [Bootstrap 5](https://getbootstrap.com/)
  - [Font Awesome](https://fontawesome.com/)
  - [JavaScript](https://developer.mozilla.org/pt-BR/docs/Web/JavaScript)

- **Banco de Dados:**
  - [MySQL](https://www.mysql.com/)

- **Ferramentas:**
  - [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/)
  - [Git](https://git-scm.com/)
  - [Entity Framework Core Tools](https://docs.microsoft.com/pt-br/ef/core/cli/dotnet)

---

## 🚀 Como Começar

Siga estas instruções para obter uma cópia do projeto em funcionamento na sua máquina local para desenvolvimento e testes.

### Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/) ou [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Instalação

1. **Clone o repositório**
   ```bash
   git clone https://github.com/ffelipemotta/SistemaMamoreGranito.git
   ```

2. **Configure o banco de dados**
   - Crie um banco de dados MySQL chamado `sistema_marmore_granito`
   - Atualize a string de conexão no arquivo `appsettings.json` com suas credenciais

3. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

4. **Execute as migrações do banco de dados**
   ```bash
   dotnet ef database update
   ```

5. **Execute o projeto**
   ```bash
   dotnet run
   ```

---

## 💻 Como Usar

1. Acesse o sistema através do navegador
2. Faça login com suas credenciais
3. No dashboard, você encontrará:
   - Visão geral do estoque
   - Acesso rápido às principais funcionalidades
   - Estatísticas atualizadas

4. Utilize o menu principal para acessar:
   - Gerenciamento de Blocos
   - Controle de Chapas
   - Processos de Serragem
   - Relatórios

---

## 📧 Contato

Link do Projeto: [https://github.com/ffelipemotta/SistemaMamoreGranito]