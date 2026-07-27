-- Catalogo inicial de exercicios.
--
-- O catalogo e global: todo usuario ve os mesmos exercicios ao entrar, sem que
-- ninguem precise cadastrar nada. Por isso ele vem junto com o schema, e nao por
-- importacao manual ou API externa paga.
--
-- Sao os exercicios classicos de academia — os que qualquer pessoa encontra em
-- qualquer sala de musculacao — com descricao de execucao completa: posicao
-- inicial, movimento, respiracao e o erro mais comum de cada um.
--
-- Idempotente por nome: reexecutar nao duplica, e um exercicio ja editado pela
-- aplicacao nao e sobrescrito.
--
-- GrupoMuscular segue EnumGrupoMuscular:
--   1 Peito | 2 Costas | 3 Pernas | 4 Ombros | 5 Biceps | 6 Triceps | 7 Abdomen
--
-- VideoUrl fica nulo de proposito: link de video quebra com o tempo e vira
-- manutencao. A descricao carrega a orientacao; os videos entram depois pelo
-- cadastro de administrador.

INSERT INTO "Exercicios" ("Nome", "GrupoMuscular", "Equipamento", "Descricao")
SELECT v."Nome", v."GrupoMuscular", v."Equipamento", v."Descricao"
FROM (VALUES
    -- ── Peito ────────────────────────────────────────────────────────────────
    ('Supino reto com barra', 1, 'Barra',
     'O exercicio mais tradicional para peitoral. Deite no banco reto com os pes firmes no chao, escapulas retraidas e um leve arco natural na lombar. Segure a barra com pegada um pouco mais aberta que a largura dos ombros. Desca controlado ate a barra tocar de leve o meio do peito, inspirando na descida, e empurre de volta expirando, sem travar os cotovelos no topo. ERRO COMUM: abrir os cotovelos a 90 graus em relacao ao tronco — mantenha-os a cerca de 45 graus para poupar o ombro. Trabalha peitoral maior, triceps e deltoide anterior.'),

    ('Supino inclinado com halteres', 1, 'Halteres',
     'Enfatiza a parte superior do peitoral, regiao que da o aspecto de peito cheio. Ajuste o banco entre 30 e 45 graus — acima disso o ombro assume o trabalho. Inicie com os halteres na altura do peito, palmas voltadas para frente. Empurre para cima e levemente para dentro, sem bater um halter no outro, e desca ate sentir o alongamento do peitoral. ERRO COMUM: inclinar o banco demais e transformar o exercicio em desenvolvimento de ombro.'),

    ('Supino declinado com barra', 1, 'Barra',
     'Foca a porcao inferior do peitoral e costuma permitir mais carga que o supino reto, por encurtar a amplitude. Deite no banco declinado com os pes travados no apoio. Desca a barra ate a parte de baixo do peito e empurre de volta. Por causa da posicao invertida da cabeca, evite cargas maximas sem alguem acompanhando. ERRO COMUM: descer a barra alto demais, no meio do peito, o que anula o objetivo do declinio.'),

    ('Crucifixo reto com halteres', 1, 'Halteres',
     'Isola o peitoral tirando o triceps do movimento. Deitado no banco reto, suba os halteres e flexione levemente os cotovelos — esse angulo fica FIXO do inicio ao fim. Abra os bracos em arco ate sentir o alongamento na linha do peito e feche como se abracasse algo grande, contraindo no topo. Use bem menos carga que no supino. ERRO COMUM: flexionar e estender os cotovelos, transformando o exercicio em supino.'),

    ('Crossover na polia', 1, 'Polia',
     'Excelente para finalizar o treino de peito, porque mantem tensao constante do inicio ao fim — diferente dos halteres, que perdem carga no topo. Fique em pe entre duas polias altas, um pe a frente do outro e tronco levemente inclinado. Traga as maos ate se cruzarem na frente do quadril e segure a contracao por um segundo. ERRO COMUM: usar carga alta e puxar com os bracos em vez de fechar com o peito.'),

    ('Flexao de bracos', 1, 'Peso corporal',
     'O classico que funciona em qualquer lugar, sem equipamento. Maos um pouco mais abertas que os ombros, corpo formando uma linha reta da cabeca aos calcanhares, abdomen e gluteo contraidos. Desca ate o peito quase encostar no chao, inspirando, e suba expirando. Para facilitar, apoie os joelhos; para dificultar, eleve os pes num banco. ERRO COMUM: deixar o quadril cair ou subir, o que quebra a linha do corpo e tira a tensao do peitoral.'),

    -- ── Costas ───────────────────────────────────────────────────────────────
    ('Barra fixa pronada', 2, 'Peso corporal',
     'Considerado o melhor exercicio para largura das costas. Segure a barra com pegada pronada (palmas para frente) mais aberta que os ombros. Antes de dobrar os cotovelos, puxe as escapulas para baixo — e esse detalhe que ativa o dorsal. Suba ate o queixo passar da barra e desca controlado ate quase estender os bracos. Se ainda nao consegue, use elastico ou a maquina assistida. ERRO COMUM: subir com impulso de perna e descer em queda livre.'),

    ('Puxada frontal na polia', 2, 'Polia',
     'A alternativa a barra fixa com carga ajustavel, ideal para quem esta comecando. Sente com as coxas firmes sob o apoio e segure a barra com pegada aberta. Incline o tronco cerca de 20 graus para tras e puxe a barra ate a parte alta do peito, aproximando as escapulas. Desca controlado. ERRO COMUM: puxar a barra atras da nuca — sobrecarrega o ombro sem nenhum beneficio extra para as costas.'),

    ('Remada curvada com barra', 2, 'Barra',
     'Constroi espessura nas costas e trabalha toda a cadeia posterior. Em pe, incline o tronco cerca de 45 graus com joelhos levemente flexionados e coluna NEUTRA — nunca arredondada. Puxe a barra em direcao ao umbigo levando os cotovelos para tras e junte as escapulas no topo. Desca controlado. ERRO COMUM: arredondar a lombar para levantar mais peso, o que e a principal causa de lesao neste exercicio.'),

    ('Remada unilateral com halter', 2, 'Halteres',
     'Trabalha um lado por vez, o que ajuda a corrigir diferencas entre os lados do corpo. Apoie joelho e mao do mesmo lado no banco, deixando a coluna paralela ao chao. Com a outra mao, puxe o halter ao lado do tronco levando o cotovelo bem para tras, como se puxasse o cabo de um cortador de grama. ERRO COMUM: girar o tronco para ajudar na subida — o quadril deve permanecer quadrado com o chao.'),

    ('Remada baixa na polia', 2, 'Polia',
     'Otimo para espessura das costas com a coluna em posicao segura. Sentado, joelhos levemente flexionados e tronco ereto, puxe o triangulo ate o abdomen mantendo os cotovelos rentes ao corpo. Segure a contracao juntando as escapulas e volte controlando o peso. ERRO COMUM: balancar o tronco para frente e para tras usando o impulso, em vez de manter o tronco parado e puxar so com as costas.'),

    ('Pulldown com bracos estendidos', 2, 'Polia',
     'Exercicio de isolamento raro para as costas: como o cotovelo nao dobra, o biceps sai quase por completo do movimento. De frente para a polia alta, bracos estendidos e cotovelos travados, empurre a barra em arco ate a altura das coxas usando so o dorsal. Excelente para quem tem dificuldade de sentir as costas trabalhando. ERRO COMUM: dobrar os cotovelos e transformar em triceps.'),

    -- ── Pernas ───────────────────────────────────────────────────────────────
    ('Agachamento livre', 3, 'Barra',
     'O exercicio mais completo para membros inferiores, trabalhando quadriceps, gluteo, posterior e todo o core. Barra apoiada no trapezio, pes na largura dos ombros com pontas levemente para fora. Inspire, desca empurrando o quadril para tras como se fosse sentar, ate a coxa ficar ao menos paralela ao chao. Suba empurrando pelos calcanhares, expirando. ERRO COMUM: deixar os joelhos caírem para dentro na subida — eles devem acompanhar a direcao dos pes.'),

    ('Leg press 45', 3, 'Maquina',
     'Permite carga alta com a coluna apoiada, sendo mais seguro que o agachamento para iniciantes. Pes na plataforma na largura dos ombros e lombar TOTALMENTE apoiada no encosto. Desca ate cerca de 90 graus de flexao do joelho e empurre sem travar os joelhos no final. Pes mais altos recrutam mais gluteo e posterior; mais baixos, mais quadriceps. ERRO COMUM: descer demais e deixar o quadril descolar do banco, o que curva a lombar sob carga.'),

    ('Cadeira extensora', 3, 'Maquina',
     'Isola o quadriceps, sem participacao de gluteo ou posterior. Sente com as costas no encosto e o rolo apoiado logo acima dos tornozelos. Estenda os joelhos ate quase a extensao completa, segure a contracao por um segundo no topo e desca devagar. Muito usado como aquecimento antes do agachamento ou como finalizacao. ERRO COMUM: subir rapido usando impulso e soltar o peso na descida, desperdicando a fase mais produtiva do movimento.'),

    ('Mesa flexora', 3, 'Maquina',
     'Principal exercicio para o posterior de coxa, musculo essencial para equilibrio articular do joelho e frequentemente esquecido. Deite de bruços com o rolo sobre os tendoes de aquiles e o quadril firme no apoio. Flexione os joelhos trazendo os calcanhares em direcao ao gluteo, segure e desca devagar. ERRO COMUM: levantar o quadril do banco para ganhar impulso — se isso acontece, a carga esta alta demais.'),

    ('Levantamento terra romeno', 3, 'Barra',
     'O melhor exercicio para posterior de coxa e gluteo em alongamento. Em pe segurando a barra, joelhos levemente flexionados e FIXOS nesse angulo. Empurre o quadril para tras deslizando a barra rente as pernas ate sentir o alongamento atras da coxa. Volte contraindo o gluteo. A coluna permanece neutra o tempo todo. ERRO COMUM: confundir com o terra convencional e agachar — aqui o movimento vem do quadril, nao do joelho.'),

    ('Afundo com halteres', 3, 'Halteres',
     'Trabalha cada perna isoladamente e desafia o equilibrio, o que o torna muito funcional. Um halter em cada mao, de um passo a frente e desca ate o joelho de tras quase encostar no chao, mantendo o tronco ereto. Empurre pelo calcanhar da perna da frente para voltar. Pode ser feito parado, alternando ou caminhando. ERRO COMUM: dar um passo curto demais, o que joga o joelho da frente muito a frente do pe.'),

    ('Panturrilha em pe', 3, 'Maquina',
     'A panturrilha responde melhor a amplitude completa e alto volume do que a carga pesada. Apoie as pontas dos pes na plataforma com os calcanhares livres no ar. Suba o maximo que conseguir ficando na ponta dos pes, segure a contracao por um segundo e desca ate sentir o alongamento total. ERRO COMUM: fazer repeticoes curtas e rapidas com muito peso, sem passar por toda a amplitude.'),

    -- ── Ombros ───────────────────────────────────────────────────────────────
    ('Desenvolvimento militar com barra', 4, 'Barra',
     'O principal construtor de massa para os ombros. Em pe ou sentado, barra na altura das clavículas com pegada pouco mais aberta que os ombros. Contraia abdomen e gluteo e empurre a barra acima da cabeca ate quase estender os cotovelos, movendo levemente a cabeca para tras para dar passagem. Desca controlado. ERRO COMUM: arquear demais a lombar para compensar a falta de mobilidade de ombro — se isso acontece, reduza a carga.'),

    ('Desenvolvimento com halteres', 4, 'Halteres',
     'Permite amplitude maior que a barra e trabalha cada lado independentemente. Sentado com encosto, halteres na altura das orelhas e palmas para frente. Empurre para cima em leve arco, aproximando os halteres no topo sem bater. Desca ate os cotovelos formarem 90 graus. ERRO COMUM: descer demais buscando amplitude extra, o que estressa a articulacao do ombro sem ganho de estimulo.'),

    ('Elevacao lateral', 4, 'Halteres',
     'O exercicio que constroi a largura dos ombros, trabalhando a porcao lateral do deltoide. Em pe, halteres ao lado do corpo e cotovelos levemente flexionados. Eleve os bracos ate a altura dos ombros LIDERANDO COM O COTOVELO, como se derramasse agua de uma jarra. Desca devagar. ERRO COMUM: usar carga pesada e balancar o tronco — este e um exercicio de isolamento e pede carga leve com execucao limpa.'),

    ('Elevacao frontal', 4, 'Halteres',
     'Trabalha a porcao anterior do deltoide, que ja recebe estimulo indireto no supino. Em pe, halteres a frente das coxas com palmas voltadas para o corpo. Eleve um braco de cada vez ate a altura dos ombros, sem passar disso, e desca controlado. Por ja ser bastante recrutada em outros exercicios, costuma pedir menos volume. ERRO COMUM: elevar acima da linha dos ombros, o que transfere a tensao para o trapezio.'),

    ('Crucifixo inverso', 4, 'Halteres',
     'Fortalece o deltoide posterior, a porcao mais negligenciada do ombro — e a que mais contribui para a postura e a saude do manguito rotador. Incline o tronco a frente com os halteres pendurados sob o peito. Abra os bracos ate a altura dos ombros aproximando as escapulas, com cotovelos levemente flexionados. ERRO COMUM: usar carga alta e puxar como remada, recrutando as costas em vez do ombro posterior.'),

    -- ── Biceps ───────────────────────────────────────────────────────────────
    ('Rosca direta com barra', 5, 'Barra',
     'O exercicio base para biceps, permitindo a maior carga entre os movimentos de flexao de cotovelo. Em pe, pegada supinada na largura dos ombros e cotovelos colados ao tronco. Flexione ate a barra chegar perto do peito, contraia no topo e desca controlado ate quase estender. ERRO COMUM: balancar o tronco para dar impulso e deixar os cotovelos irem a frente — se precisar disso, a carga esta alta demais.'),

    ('Rosca alternada com halteres', 5, 'Halteres',
     'A supinacao do punho durante a subida aumenta a ativacao do biceps, que tem justamente a funcao de girar o antebraco. Em pe, halteres ao lado do corpo com pegada neutra. Suba um braco de cada vez girando o punho para supinado ao longo da subida. Desca desfazendo o giro. ERRO COMUM: comecar ja supinado, o que elimina a principal vantagem deste exercicio.'),

    ('Rosca martelo', 5, 'Halteres',
     'Pegada neutra do inicio ao fim, como quem segura um martelo. Enfatiza o braquial e o braquiorradial, musculos que ficam sob e ao lado do biceps — desenvolve-los aumenta a espessura visual do braco. Cotovelos fixos ao lado do tronco, suba ate a altura do ombro e desca devagar. ERRO COMUM: girar o punho durante o movimento, o que descaracteriza o exercicio.'),

    ('Rosca scott', 5, 'Maquina',
     'O banco inclinado elimina qualquer possibilidade de impulso, tornando este o exercicio mais rigoroso para biceps. Apoie os bracos no banco com as axilas encostadas na borda superior. Flexione sem tirar os cotovelos do apoio e desca ate quase estender — mas com cuidado na fase final, que e a de maior alongamento. ERRO COMUM: soltar o peso na descida, justamente onde o risco de estiramento e maior.'),

    ('Rosca concentrada', 5, 'Halteres',
     'Excelente para conexao mente-musculo e para trabalhar o pico do biceps. Sentado, apoie o cotovelo na parte interna da coxa e deixe o braco pendendo. Flexione ate proximo do ombro concentrando toda a atencao na contracao, segure um instante e desca devagar. Costuma ser usado no final do treino. ERRO COMUM: usar carga alta e empurrar o cotovelo contra a coxa para ajudar.'),

    -- ── Triceps ──────────────────────────────────────────────────────────────
    ('Triceps testa com barra W', 6, 'Barra',
     'Trabalha a cabeca longa do triceps, a maior das tres e a que mais contribui para o volume do braco. Deitado no banco, leve a barra acima da testa com os cotovelos apontados para o teto. Flexione APENAS os cotovelos, descendo a barra ate perto da testa, e estenda de volta. A barra W poupa os punhos em relacao a barra reta. ERRO COMUM: mover os bracos junto com os antebracos, transformando o movimento em pullover.'),

    ('Triceps na polia com corda', 6, 'Polia',
     'O mais popular para triceps, com tensao constante e facil de executar bem. De frente para a polia alta, cotovelos colados ao tronco e antebracos paralelos ao chao. Estenda os cotovelos e, no final do movimento, afaste as pontas da corda para intensificar a contracao. Volte controlado. ERRO COMUM: deixar os cotovelos irem para frente ou inclinar o tronco sobre a barra, usando o peso do corpo.'),

    ('Triceps frances com halter', 6, 'Halteres',
     'A posicao com o braco acima da cabeca alonga a cabeca longa do triceps, gerando um estimulo diferente dos demais exercicios. Sentado ou em pe, segure um halter com as duas maos atras da cabeca. Estenda os cotovelos ate quase travar e desca controlado sentindo o alongamento. Mantenha o abdomen firme para nao arquear a lombar. ERRO COMUM: abrir os cotovelos para os lados, o que reduz a tensao no triceps.'),

    ('Mergulho entre bancos', 6, 'Peso corporal',
     'Usa o peso do proprio corpo e pode ser feito em qualquer lugar com dois apoios. Maos na borda de um banco atras do corpo, dedos apontados para frente, pernas estendidas. Desca flexionando os cotovelos ate cerca de 90 graus e empurre de volta. Para dificultar, apoie os pes em outro banco ou coloque um peso sobre as coxas. ERRO COMUM: descer alem do confortavel, o que coloca o ombro em posicao vulneravel.'),

    ('Triceps coice', 6, 'Halteres',
     'Exercicio de isolamento em que o triceps atinge contracao maxima com o braco alinhado ao tronco. Incline o tronco, cole o braco ao corpo e mantenha o cotovelo a 90 graus. Estenda o cotovelo para tras ate o braco ficar totalmente reto e segure a contracao. Use carga leve. ERRO COMUM: balancar o antebraco usando impulso — o movimento perde todo o valor se nao for controlado.'),

    -- ── Abdomen ──────────────────────────────────────────────────────────────
    ('Prancha isometrica', 7, 'Peso corporal',
     'O exercicio mais eficiente para estabilidade do core, trabalhando o abdomen na sua funcao real: manter o tronco firme. Apoie antebracos e pontas dos pes, formando uma linha reta da cabeca aos calcanhares. Contraia abdomen e gluteo e respire normalmente durante a sustentacao. Comece com 20 a 30 segundos e progrida no tempo. ERRO COMUM: elevar o quadril, o que facilita o exercicio e reduz o estimulo, ou deixá-lo cair, o que sobrecarrega a lombar.'),

    ('Abdominal supra no solo', 7, 'Peso corporal',
     'O abdominal classico, focado na porcao superior do reto abdominal. Deite com joelhos flexionados e maos ao lado da cabeca SEM entrelaçar os dedos atras do pescoco. Eleve o tronco ate as escapulas saírem do chao, expirando, e desca controlado. O movimento e curto: nao e preciso sentar por completo. ERRO COMUM: puxar a cabeca com as maos, o que forca o pescoco sem envolver mais o abdomen.'),

    ('Elevacao de pernas suspenso', 7, 'Peso corporal',
     'Um dos exercicios mais exigentes para a porcao inferior do abdomen. Pendurado na barra fixa, estabilize o corpo sem balancar. Eleve as pernas ate a altura do quadril ou acima, enrolando levemente a pelve no final — e esse detalhe que ativa o abdomen em vez do flexor de quadril. Desca devagar. ERRO COMUM: usar embalo e deixar o corpo oscilar entre as repeticoes.'),

    ('Abdominal infra no solo', 7, 'Peso corporal',
     'Versao acessivel para trabalhar a porcao inferior do abdomen, sem precisar de barra. Deite com as maos sob o quadril para proteger a lombar e as pernas estendidas. Eleve as pernas ate a vertical e desca sem deixar os calcanhares encostarem no chao, mantendo a tensao. ERRO COMUM: deixar a lombar descolar do chao na descida, o que transfere o esforco para os flexores do quadril.'),

    ('Prancha lateral', 7, 'Peso corporal',
     'Trabalha os obliquos e a estabilidade lateral do tronco, complementando a prancha tradicional. Apoie um antebraco e a lateral do pe, eleve o quadril e mantenha o corpo alinhado, sem rotacionar. Sustente pelo tempo determinado e repita do outro lado. Para facilitar, apoie o joelho de baixo no chao. ERRO COMUM: deixar o quadril cair em direcao ao chao ao longo da sustentacao.'),

    ('Abdominal na polia alta', 7, 'Polia',
     'A grande vantagem sobre os abdominais livres e permitir progressao de carga, igual a qualquer outro exercicio. Ajoelhado de costas para a polia alta, segure a corda ao lado da cabeca. Flexione o tronco levando os cotovelos em direcao aos joelhos usando o ABDOMEN, com o quadril fixo. Volte controlado. ERRO COMUM: puxar com os bracos ou sentar para tras, usando o peso do corpo em vez da contracao abdominal.')
) AS v("Nome", "GrupoMuscular", "Equipamento", "Descricao")
WHERE NOT EXISTS (
    SELECT 1 FROM "Exercicios" e WHERE e."Nome" = v."Nome"
);
