// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");
List<char> array = new List<char>{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
var elementos = array.OrderBy(x => Guid.NewGuid()).ToList();
List<Tuple<char, char>> paresJaSaidos = new();
List<int> tamanhoGrupos = new();
List<List<char>> grupos = new();
int groupSize = 3; //tamanho dos grupos que queremos
int minGroupSize = 2;

tamanhoGrupos = ObterTamanhoGrupos(elementos, groupSize, minGroupSize);
grupos = sortearGroups(elementos);

for (int i = 0; i < tamanhoGrupos.Count(); i++) //percorrer os grupos
{
    //Console.WriteLine("for (int i = 0; i < tamanhoGrupos.Count(); i++)");
    
    List<char> elementosDoGrupo = new();

    for (int j = 0; j < tamanhoGrupos[i]; j++) //percorrer cada elemento do grupo para o preencher
    {
        //Console.WriteLine("for (int j = 0; j < tamanhoGrupos[i]; j++)");
        if(j==0) //Se é o primeiro elemento apenas seleciona o primeiro
        {
            //Console.WriteLine("(j==0) ");
            elementosDoGrupo.Add(elementos.FirstOrDefault());
            elementos.Remove(elementos.FirstOrDefault());
            //continue;
        }
        else //Se não é o primeiro vamos fazer a verificacação do que repete menos vezes com os elementos já selecionados
        {
            Dictionary<char,int> elementsRepetitionsInCurrentGroups = new();
            for (int y = 0; y < elementos.Count(); y++) //percorrer todos os elementos ainda não atribuidos dos elementos a atribuir a grupos para ver o que repete menos vezes dentro do grupo no ciclo
            {
                elementsRepetitionsInCurrentGroups[elementos[y]] = 0;
                char itemSelecionado = elementos[y]; //Selecionar o elemento do ciclo actual
                for (int t = 0; t < j; t++) //combinar este elemento com cada um dos elementos já selecionados para este grupo
                {
                    Tuple<char, char> tuple = new(elementosDoGrupo[t], itemSelecionado);
                    Tuple<char, char> tupleReversed = new(itemSelecionado, elementosDoGrupo[t]);
                    bool existe = paresJaSaidos.Contains(tuple);
                    if(!existe) existe = paresJaSaidos.Contains(tupleReversed);
                    if(existe)
                    {
                        elementsRepetitionsInCurrentGroups[elementos[y]]++;
                    }
                }
            }
            var elementoAAtribuir = elementsRepetitionsInCurrentGroups.OrderBy(x=>x.Value).FirstOrDefault().Key; //escolher o elemento que repetiu menos vezes
            elementosDoGrupo.Add(elementoAAtribuir); //Adicionar aos elementos do grupo
            elementos.Remove(elementoAAtribuir); //E remover dos elementos a atribuir a grupos uma vez que já está atribuido
            for (int t = 0; t < j; t++) //combinar NOVAMENTE este elemento com cada um dos elementos já selecionados para este grupo para adicionar os pares aobtidos à lista dos já saídos
            {
                Tuple<char, char> tuple = new(elementosDoGrupo[t], elementoAAtribuir);
                paresJaSaidos.Add(tuple);
            }
        }
        
    }
    grupos.Add(elementosDoGrupo);
    if (i == tamanhoGrupos.Count() - 1)
    {
        i = -1;
        elementos = array.OrderBy(x => Guid.NewGuid()).ToList();
    }
    if(grupos.Count()==15)
    {
        i = 5;
    }
    
}

for (int i = 0; i < grupos.Count(); i++) //Listar as combinaçoes obtidas
{
    string grupo = "";
    for (int j = 0; j < grupos[i].Count(); j++) //percorrer cada elemento do grupo para o preencher
    {
        grupo += grupos[i][j]+",";
    }
    Console.WriteLine("Grupo:"+grupo);
}
Console.ReadKey();


        //HashSet<Tuple<char, char>> charPairs = new HashSet<Tuple<char, char>>();
        //charPairs.Add(new Tuple<char, char>('a', 'b'));


        //// Display the unique char pairs found in the input string
        //foreach (var pair in charPairs)
        //{
        //    Console.WriteLine("({0}, {1})", pair.Item1, pair.Item2);
        //}


List<List<char>> sortearGroups(List<char> elements)
{

    List<List<char>> k = new();
    return k;
}


List<int> ObterTamanhoGrupos(List<char> elementos, int groupSize = 3, int minGroupSize = 2)
{
    int lastGroupSize = 3;  //o tamanho do ultimo grupo a sortear
    int groupsCount = elementos.Count() / groupSize; //calc 1
    int totalElementosEmGrupos = groupSize * groupsCount; //calc 2
    int elementosSemGrupo = elementos.Count() - totalElementosEmGrupos; //numero de elementos sem grupo
    if (elementosSemGrupo > 0) //Se existirem elementos sem grupo
    {
        if (elementosSemGrupo == 1) //Se o número de elemento sem grupo for de apenas um, acrescenta-se esse elemento ao último grupo
        {
            lastGroupSize += 1;
        }
        else //Se o número de elementos sem grupo for superior a 1 fazem um grupo de 2 ou mais
        {    //AQUI PODERIA INTRODUZIR UMA NOVA FUNCIONALIDADE QUE ERA A DO TAMANHO MÍNIMO DO GRUPO.
             //SE OS ELEMENTO DE FORA DO GRUPO NÃO FOSSEM IGUAL OU SUPERIOR A ESSE LIMITE ENTÃO TERIA QUE DISPERSAR UM A UM ESSES ELEMENTOS PELOS GRUPOS ANTERIORES
            groupsCount += 1;
            lastGroupSize = elementosSemGrupo;
        }
    }

    List<int> tamanhoDosgrupos = new List<int>();
    for (int i = 0; i < groupsCount; i++)
    {
        int tamanho = groupSize;
        
        tamanhoDosgrupos.Add(tamanho);
    }

 
    if (elementosSemGrupo == 1) //e existe apenas umm elemento sem grupo
    {
        tamanhoDosgrupos[tamanhoDosgrupos.Count-1] = groupSize + 1; //esse último grupo vai ter mais um elemento
    }
    else
    {
        //Agora, se os elementos sem grupo forem mais que o grupo mínimo, adiciona-se um novo grupo com essa quantidade de elementos
        if (elementosSemGrupo > minGroupSize)
        {
            tamanhoDosgrupos.Add(elementosSemGrupo);
        }
        else //caso contrário, vamos distribuir esses elementos, um a um, pelos grupo, acrescentando um elemento a cada grupo, até esgotarem
        {
            for (int s = 0; s < elementosSemGrupo; s++)
            {
                tamanhoDosgrupos[s] += 1;
            }
        }
    }
  
    
    return tamanhoDosgrupos;
}



























void sortChars(List<char> elements, HashSet<Tuple<char, char>> charPairs)
{
    

    List<List<char>> grupos = new();
    int indexGrupo = 0;
    foreach (var tamanhoGrupo in tamanhoGrupos)
    {
        List<char> chars= new List<char>();
        //Aqui começo a preencher o grupo de 3 elementos, mais ou menos
        for (int i = 0; i < tamanhoGrupo; i++)
        {
            if (elements.Count()==0)
            {
                Console.WriteLine("Sem mais elemento para sortear");
                break;
            }

            //Selecionar o primeiro
            if(i==0)
            {
                chars[i]=elements.FirstOrDefault();
                elements.Remove(chars[i]);
            }

            //Selecionar o segundo
            if(i==1)
            {
                foreach (var item in elements)
                {
                    char char1 = chars[i];
                    char char2 = item;
                    Tuple<char, char> jh =new Tuple<char, char>(char1, char2);
                    //Verificar se este par já foi sorteado
                    bool contains = charPairs.Contains(jh);
                    if (contains)
                    {
                        //move next
                        int index = elements.IndexOf(item);
                        if(index < elements.Count()-1)
                        {

                        }
                    }
                    else
                    {
                        //Add the char to the second group element
                        chars[i] = item;
                        elements.Remove(chars[i]);
                    }
                }
            }

            //Selecionar o terceiro
        }
        indexGrupo++;
        if (elements.Count() == 0)
        {
            Console.WriteLine("Sem mais elemento para sortear - definitivo");
            break;
        }
    }
   


    


    //for (int i = 0; i < elements.Length; i += groupSize)
    //{
    //    char[] group = elements.Skip(i).Take(groupSize).ToArray();
    //    Console.WriteLine(string.Join(",", group));

    //}

}