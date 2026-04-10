// Models/Produto.cs — versão atualizada com relacionamento 1-para-N

namespace DesenvWebApi.Models;

public class Produto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // =====================================================================
    // CHAVE ESTRANGEIRA — lado "muitos" do relacionamento
    //
    // CategoriaId é uma chave estrangeira (Foreign Key) que referencia
    // a tabela Categorias. No banco de dados, o EF vai criar:
    //   FOREIGN KEY ("CategoriaId") REFERENCES "Categorias"("Id")
    //
    // Esta é a COLUNA REAL que será adicionada à tabela Produtos.
    // Ela armazena apenas o ID da categoria (um número inteiro),
    // não a categoria inteira.
    //
    // Por que "int" e não "int?"?
    // Porque a categoria é OBRIGATÓRIA para um produto.
    // Se fosse opcional, usaríamos "int?" (nullable).
    //
    // Exemplo: se o produto "Notebook Dell" pertence à categoria
    // "Eletrônicos" (Id = 1), então CategoriaId = 1.
    // =====================================================================
    public int CategoriaId { get; set; }

    // =====================================================================
    // PROPRIEDADE DE NAVEGAÇÃO — permite acessar o objeto Categoria inteiro
    //
    // Esta propriedade NÃO corresponde a uma coluna no banco de dados.
    // O EF usa o par (CategoriaId + Categoria) para entender o relacionamento.
    //
    // Por que "?" (nullable)?
    // Porque ao criar um Produto pelo código, você pode definir apenas
    // o CategoriaId (um número) sem carregar o objeto Categoria inteiro.
    // O EF só preenche esta propriedade quando você usa .Include() na query.
    //
    // Sem .Include(): produto.Categoria é null
    // Com .Include(): produto.Categoria é o objeto completo { Id, Nome, ... }
    //
    // Isso é chamado de Lazy vs Eager Loading (veremos em detalhes mais adiante).
    //
    // Uso: produto.Categoria.Nome, produto.Categoria.Id, etc.
    // (só disponível após .Include() na query)
    // =====================================================================
    public Categoria? Categoria { get; set; }

    // Adicione esta propriedade ao final da classe Produto,
    // DEPOIS das propriedades CategoriaId e Categoria que já existem:

    // =====================================================================
    // PROPRIEDADE DE NAVEGAÇÃO para o DetalheProduto (relacionamento 1-para-1)
    //
    // Diferente do 1-para-N (onde usamos ICollection<T>),
    // no 1-para-1 usamos o tipo diretamente: DetalheProduto?
    // Porque um produto tem NO MÁXIMO UM detalhe (não uma coleção).
    //
    // É nullable (?) porque nem todo produto precisa ter um detalhe cadastrado.
    // Um Notebook pode ter especificações técnicas detalhadas,
    // mas um pacote de Arroz provavelmente não precisa.
    //
    // Só é preenchida quando usamos .Include(p => p.DetalheProduto) na query.
    // =====================================================================
    public DetalheProduto? DetalheProduto { get; set; }
}