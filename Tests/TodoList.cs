
//TODO: 0001005. Gunnar & Tanel - Termodünaamilisel temperatuuril ei näita ühikute teisendamist, sest see on funktsiooniga ja funktsiooni ei näita, see on praegu 
//               täiesti tegemata (Kui uniti type on numbriga factored või derivate siis näitab aga kui on function siis ei näita)

//TODO: 0001006. Kaur & Rainer - Measure loomisel võiks saada kohe create new alt valida mis tüüpi measure tuleb (Base, Derived,) (nagu kontaktil)
//              Base->Name, Code, Definition, Valid From, Valid To
//              Derived-> Name, Code, Definition, Valid From, Valid To ja kohe peab alla tulema ka need read kust saad hakata valima 
//              mõnda teist measuret ning siis aste, kui meil on olemas Mass, Pikkus ja Aeg ning tahame lisada Kiirust, siis saame 
//              lisada Measure mis on derived ja saame valida et pikkus astmes 1 korda aeg astmes 1 (ehk siis v=l/t) ja siis automaatselt teeb vastava baasühiku valmis,
//              võtab pikkuse baasühiku mille faktor on 1 ja aja baasühiku mille faktor on 1 ja teeb siis näiteks meeter/sekundis


//TODO: 0001007.Measurel (ka Unititel jne, mingit abstraktset lahendust tarvis) võiks veel olla see, et kui praegu on Show Units ja Show Terms, siis põhimõtteliselt 
//              võiks need ka olla kohe siin olemas, Kui lähen Acidity alla siis seal kohe näitab ära millised on tema ühikud ja siis põhimõtteliselt kuidas teda arvutatakse 
//              (Nii Details, Delete kui ka Edit -> seal saab kohe Terme ja Uniteid lisada/muuta)  
//TODO: 0001008.Measure Termidelt kindlasti back to full list ära
//TODO: 0001009.Unit terms back to full list ära
//TODO: 0001010.Kui ma lähen Measurest Unititesse listist, siis ma pean tagasi tulema listi, kui ma lähen Measure Detailist Unititesse, siis ma pean tagasi tulema Detaili
//TODO: 0001011.Definitsioonide redigeerimisel Measure all, kus on pikk tekst, siis võiks olla mingi normaalsem kast seal (venitatav kast näiteks, adapteeruv multiline)
//TODO: 0001012.Millegi pärast listide lehel (index) navigaatorid ja tabel on väikese nihkega võrreldes searchi reaga, Firstist alates natuke vales kohas terve nimekiri)
//TODO: 0001013.Kõikidel termodünaamika unititel ta ei näita reegleid ja need reeglid peavad olema samamoodi heasti lisatavad nagu punktis 7. seletatud detailide redigeerimisel

//TODO: 0001014. Vaadata läbi kõik Quantity Data diagrammid
//TODO: 0001015. Vaadata läbi kõik Quantity Domain diagrammid
//TODO: 0001016. Vaadata läbi kõik Quantity Facade diagrammid

//TODO: 0001017. Leht venitades ja kokku lükates ei käitu õigesti

//TODO: 0001018. Analoogiliselt Unique Attributele tuleb teha Concurrency Controli attribute, et Timestampi järgi läheme muutma ainult siis, 
//              kui keegi ei ole vahepeal muutnud ja kustutame ainult siis, kui keegi ei ole vahepeal muutnud, kustutanud, kustutada ja muuta nii 
//              et valid from muudetakse ära ja andmeid näidatakse ainult siis kui valid from on kehtiv (või et meie indeksis peame nägema, 
//              kuid kui valid from on möödas siis ei tohiks selectlistidesse minna, et teda ei saa järgmisesse objekti kusagile määrata)


//TODO: 0002001. RulesPages CanClickCreateButtonTest kukub vahepeal läbi
//TODO: 0002002. Kui testid käivitada, siis mingi jama on testidega ja ta annab koguaeg mingi 6 testi mis on helesinised, 
//              need on OrderedRepo testid ning põhjus on DynamicDatas ilmselt, tuleb süveneda
//TODO: 0002003. Liiga pikalt laadib alla Currenciesi
//TODO: 0002004. (Pikas perspektiivis) Measure ja Unitite valemid võiks olla paremas loetavas vormingus, on olemas mingisugune "openMath" ja "MathML"
