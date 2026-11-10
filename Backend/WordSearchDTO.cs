using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    // record: veri taşımak için tasarlanmış tip. Parantez içindekiler hem constructor parametresi
    // hem de property olur. Bu property'ler "init" olduğu için nesne oluşturulduktan sonra DEĞİŞTİRİLEMEZ.

    // record: en type designet til at transportere data. Parametrene i parentesen er både constructor-parametre 
    // og properties. Da disse properties er "init", kan de IKKE ændres, efter objektet er oprettet.
    public record WordSearchDTO(
        string SearchWord,
        WordCount MatchCount, // Vores egen type i stedet for int
        ElapsedTime ElapsedMilliseconds // Vores egen type i stedet for double
    );

}
