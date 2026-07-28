-- Segunda leva do catalogo: 35 exercicios classicos que completam os grupos.
--
-- Mesma regra do 08: exercicios que existem em qualquer academia, com descricao
-- de execucao completa e o erro mais comum destacado. Idempotente por nome.
--
-- GrupoMuscular: 1 Peito | 2 Costas | 3 Pernas | 4 Ombros | 5 Biceps | 6 Triceps | 7 Abdomen

INSERT INTO "Exercicios" ("Nome", "GrupoMuscular", "Equipamento", "Descricao")
SELECT v."Nome", v."GrupoMuscular", v."Equipamento", v."Descricao"
FROM (VALUES
    -- ── Peito ────────────────────────────────────────────────────────────────
    ('Supino reto com halteres', 1, 'Halteres',
     'Alternativa ao supino com barra que permite amplitude maior e corrige diferencas entre os lados. Deitado no banco reto, inicie com os halteres na altura do peito e palmas voltadas para frente. Empurre para cima aproximando os halteres sem bate-los e desca ate sentir o alongamento do peitoral. Exige mais estabilizacao que a barra, entao use carga um pouco menor. ERRO COMUM: descer os halteres desalinhados ou baixo demais, o que estressa o ombro.'),

    ('Voador peck deck', 1, 'Maquina',
     'Isola o peitoral com trajetoria guiada, sendo o exercicio mais simples de executar bem entre os de peito. Sente com as costas totalmente apoiadas e antebracos nos apoios acolchoados. Feche os bracos ate as maos se aproximarem na frente do peito e segure a contracao por um segundo. Ideal para iniciantes ou para finalizar o treino. ERRO COMUM: usar carga alta e voltar rapido, perdendo a fase de alongamento que e a mais valiosa aqui.'),

    ('Supino reto na maquina', 1, 'Maquina',
     'Versao guiada do supino, segura para treinar ate a falha sem precisar de alguem acompanhando. Ajuste o banco para que as manoplas fiquem na altura do meio do peito. Empurre ate quase estender os cotovelos e volte controlado ate sentir o peitoral alongar. Otimo para quem esta comecando e ainda nao domina o supino livre. ERRO COMUM: ajustar o banco alto demais, o que transfere o trabalho para o ombro.'),

    ('Crucifixo inclinado com halteres', 1, 'Halteres',
     'Combina o isolamento do crucifixo com a enfase na porcao superior do peito. Banco entre 30 e 45 graus, cotovelos levemente flexionados e fixos nesse angulo. Abra os bracos em arco ate sentir o alongamento e feche como se abracasse. Use carga leve: a alavanca aqui e desfavoravel. ERRO COMUM: dobrar os cotovelos durante o movimento, transformando em supino inclinado.'),

    ('Pullover com halter', 1, 'Halteres',
     'Exercicio classico que trabalha peitoral e dorsal ao mesmo tempo, alem de contribuir para a expansao da caixa toracica. Deitado no banco, segure um halter com as duas maos acima do peito. Desca em arco por tras da cabeca ate sentir o alongamento e volte contraindo. Mantenha os cotovelos levemente flexionados e fixos. ERRO COMUM: descer alem da mobilidade do ombro em busca de amplitude.'),

    -- ── Costas ───────────────────────────────────────────────────────────────
    ('Barra fixa supinada', 2, 'Peso corporal',
     'Pegada supinada (palmas voltadas para o corpo) na largura dos ombros. A posicao coloca o biceps em vantagem mecanica, o que torna esta versao mais facil que a pronada e otima para quem esta evoluindo para a barra fixa. Puxe ate o queixo passar da barra e desca controlado. ERRO COMUM: iniciar o movimento so com os bracos — puxe as escapulas para baixo antes de dobrar os cotovelos.'),

    ('Puxada com pegada neutra', 2, 'Polia',
     'Pegada neutra (palmas frente a frente) no triangulo ou na barra V. E a posicao mais confortavel para o ombro e permite otima contracao do dorsal. Sentado com as coxas travadas, puxe ate a parte alta do peito mantendo o tronco quase ereto. ERRO COMUM: inclinar demais o tronco para tras, o que transforma o exercicio numa remada.'),

    ('Remada cavalinho', 2, 'Barra',
     'Excelente para espessura das costas, com a pegada neutra permitindo carga alta e boa contracao. Posicione-se sobre a barra com joelhos flexionados, tronco inclinado e coluna neutra. Puxe a barra em direcao ao abdomen juntando as escapulas e desca controlado. ERRO COMUM: ficar em pe demais, o que reduz a amplitude e joga o esforco para o trapezio.'),

    ('Levantamento terra convencional', 2, 'Barra',
     'Um dos exercicios mais completos que existem, recrutando costas, gluteo, posterior de coxa e todo o core. Pes na largura do quadril, barra rente as canelas, coluna neutra e peito aberto. Levante empurrando o chao com os pes e estendendo quadril e joelhos ao mesmo tempo, mantendo a barra colada ao corpo. ERRO COMUM: arredondar a lombar no inicio da subida — se isso acontece, reduza a carga imediatamente.'),

    ('Encolhimento com halteres', 2, 'Halteres',
     'Exercicio direto para o trapezio superior, responsavel pelo volume entre pescoco e ombros. Em pe, halteres ao lado do corpo com bracos estendidos. Eleve os ombros na direcao das orelhas o maximo que conseguir, segure a contracao por um segundo e desca controlado. ERRO COMUM: girar os ombros para tras durante o movimento, o que nao aumenta a ativacao e desgasta a articulacao.'),

    ('Face pull na polia', 2, 'Polia',
     'Um dos melhores exercicios para saude do ombro e postura, trabalhando deltoide posterior, trapezio medio e rotadores. Polia na altura do rosto, corda com pegada pronada. Puxe em direcao a testa afastando as pontas da corda e girando os ombros para tras. Use carga leve e alto numero de repeticoes. ERRO COMUM: puxar como remada, com os cotovelos baixos, em vez de manter os cotovelos na altura dos ombros.'),

    -- ── Pernas ───────────────────────────────────────────────────────────────
    ('Agachamento hack', 3, 'Maquina',
     'Trajetoria guiada que permite focar no quadriceps sem exigir tanto equilibrio quanto o agachamento livre. Costas e quadril apoiados no encosto, pes na plataforma na largura dos ombros. Desca ate a coxa ficar paralela ao chao e suba empurrando pelos calcanhares. Pes mais a frente reduzem o estresse no joelho. ERRO COMUM: deixar os calcanhares subirem na descida.'),

    ('Elevacao pelvica', 3, 'Barra',
     'O exercicio mais eficiente para gluteo, com a maior ativacao registrada entre os movimentos de quadril. Costas apoiadas num banco, barra sobre o quadril com protecao. Empurre o quadril para cima ate o tronco ficar paralelo ao chao, contraindo forte o gluteo no topo, e desca controlado. ERRO COMUM: hiperestender a lombar no topo em vez de contrair o gluteo — o movimento termina quando o quadril alinha com o tronco.'),

    ('Agachamento bulgaro', 3, 'Halteres',
     'Trabalha uma perna por vez com grande amplitude, sendo um dos exercicios mais completos para quadriceps e gluteo. Apoie o peito do pe de tras num banco e desca com a perna da frente ate o joelho de tras quase tocar o chao. Tronco mais ereto enfatiza o quadriceps; levemente inclinado a frente, o gluteo. ERRO COMUM: posicionar o pe da frente perto demais do banco, o que trava o movimento.'),

    ('Cadeira abdutora', 3, 'Maquina',
     'Isola o gluteo medio, musculo essencial para estabilidade do quadril e do joelho — e um dos mais esquecidos no treino. Sentado com as almofadas na parte externa das coxas, abra as pernas contra a resistencia, segure a contracao e volte devagar. Inclinar levemente o tronco a frente aumenta a ativacao. ERRO COMUM: usar impulso e amplitude parcial com carga alta.'),

    ('Panturrilha sentado', 3, 'Maquina',
     'Com o joelho flexionado, este exercicio enfatiza o soleo, musculo profundo da panturrilha que o exercicio em pe recruta menos. Sentado com as almofadas sobre as coxas e pontas dos pes na plataforma, suba o maximo possivel e desca ate o alongamento completo. Complementa a panturrilha em pe. ERRO COMUM: amplitude curta — a panturrilha responde a amplitude completa e alto volume.'),

    -- ── Ombros ───────────────────────────────────────────────────────────────
    ('Desenvolvimento Arnold', 4, 'Halteres',
     'Variacao criada por Arnold Schwarzenegger que adiciona rotacao ao desenvolvimento, recrutando as porcoes anterior e lateral do deltoide num movimento so. Comece com os halteres na altura do peito e palmas voltadas para voce. Suba girando os punhos ate as palmas ficarem para frente no topo. Desca desfazendo o giro. ERRO COMUM: girar rapido demais, perdendo a tensao no meio do movimento.'),

    ('Elevacao lateral na polia', 4, 'Polia',
     'Vantagem sobre os halteres: a polia mantem tensao constante inclusive no inicio do movimento, onde o halter praticamente nao oferece resistencia. Em pe ao lado da polia baixa, pegue o cabo com a mao oposta e eleve o braco ate a altura do ombro. Desca devagar. ERRO COMUM: afastar-se demais da polia, o que muda o angulo de resistencia.'),

    ('Remada alta com barra', 4, 'Barra',
     'Trabalha deltoide lateral e trapezio simultaneamente. Em pe, pegada pronada um pouco mais aberta que os ombros. Puxe a barra rente ao corpo ate a altura do peito, liderando com os cotovelos, que devem ficar acima das maos. Desca controlado. Pegada muito fechada aumenta o risco de impacto no ombro — prefira a pegada mais aberta. ERRO COMUM: subir a barra ate o queixo com pegada estreita.'),

    ('Desenvolvimento na maquina', 4, 'Maquina',
     'Versao guiada do desenvolvimento, segura para treinar proximo da falha e boa para iniciantes. Ajuste o banco para que as manoplas fiquem na altura dos ombros. Empurre para cima ate quase estender os cotovelos e volte controlado ate 90 graus. ERRO COMUM: descer alem do necessario buscando amplitude, colocando o ombro em posicao vulneravel.'),

    ('Elevacao frontal com barra', 4, 'Barra',
     'Versao bilateral da elevacao frontal, que permite mais carga e exige mais do core para estabilizar. Em pe, barra a frente das coxas com pegada pronada na largura dos ombros. Eleve ate a altura dos ombros mantendo os bracos quase estendidos e desca controlado. ERRO COMUM: usar impulso de quadril e tronco para iniciar o movimento.'),

    -- ── Biceps ───────────────────────────────────────────────────────────────
    ('Rosca inversa com barra', 5, 'Barra',
     'Pegada pronada (palmas para baixo) que desloca o trabalho para o braquiorradial e os extensores do antebraco. Fortalece a pegada e complementa o desenvolvimento do braco. Em pe, cotovelos colados ao tronco, flexione ate a altura do peito e desca devagar. Exige carga bem menor que a rosca direta. ERRO COMUM: usar a mesma carga da rosca direta e compensar com impulso.'),

    ('Rosca na polia baixa', 5, 'Polia',
     'A polia mantem tensao constante em toda a amplitude, inclusive no topo, onde a barra livre alivia. De frente para a polia baixa, cotovelos fixos ao lado do corpo, flexione ate proximo do peito e desca controlado. Excelente para finalizar o treino de biceps. ERRO COMUM: recuar o corpo para ajudar na subida, usando o peso corporal.'),

    ('Rosca no banco inclinado', 5, 'Halteres',
     'Com o banco inclinado a cerca de 45 graus, os bracos ficam atras da linha do tronco, colocando a cabeca longa do biceps em alongamento maximo. Isso gera um estimulo que nenhuma outra rosca oferece. Deixe os bracos pendendo e flexione sem mover os cotovelos a frente. ERRO COMUM: levantar os cotovelos durante a subida, anulando a vantagem da posicao.'),

    ('Rosca 21', 5, 'Barra',
     'Tecnica de intensificacao: 7 repeticoes da metade de baixo ate o meio, 7 do meio ate o topo e 7 completas, sem descanso entre elas. O acumulo de tensao gera muita congestao no biceps. Use carga bem menor que a da rosca direta convencional. ERRO COMUM: comecar com carga alta e nao conseguir completar as 21 repeticoes com boa execucao.'),

    -- ── Triceps ──────────────────────────────────────────────────────────────
    ('Paralelas', 6, 'Peso corporal',
     'Um dos melhores exercicios para triceps com peso corporal. Apoie-se nas barras paralelas com os bracos estendidos e mantenha o tronco o mais VERTICAL possivel para focar o triceps — inclinar a frente transfere o trabalho para o peitoral. Desca ate os cotovelos formarem 90 graus e empurre de volta. ERRO COMUM: descer demais, o que sobrecarrega a articulacao do ombro.'),

    ('Triceps na polia com barra reta', 6, 'Polia',
     'Versao com barra do triceps na polia, que permite mais carga que a corda por envolver as duas maos numa pegada fixa. De frente para a polia alta, cotovelos colados ao tronco, estenda ate os bracos ficarem retos e volte controlado ate 90 graus. ERRO COMUM: inclinar o tronco sobre a barra e empurrar com o peso do corpo em vez do triceps.'),

    ('Supino fechado', 6, 'Barra',
     'Supino com pegada na largura dos ombros, que transfere grande parte do trabalho para o triceps mantendo a possibilidade de carga alta. Deitado no banco, desca a barra ate a parte baixa do peito com os cotovelos rentes ao corpo e empurre de volta. ERRO COMUM: fechar demais a pegada, o que estressa os punhos sem aumentar a ativacao do triceps.'),

    ('Triceps unilateral na polia', 6, 'Polia',
     'Trabalha um braco por vez com pegada supinada, permitindo corrigir diferencas entre os lados e alcancar contracao maxima. De frente para a polia alta, segure a manopla com uma mao, cotovelo colado ao tronco, e estenda ate o braco ficar reto. ERRO COMUM: girar o tronco para ajudar — mantenha o corpo quadrado com a polia.'),

    ('Triceps na maquina', 6, 'Maquina',
     'Trajetoria guiada que isola o triceps sem exigir estabilizacao, sendo segura para treinar ate a falha. Ajuste o assento para que os cotovelos fiquem alinhados com o eixo da maquina e apoiados no suporte. Estenda ate quase travar e volte controlado. ERRO COMUM: ajustar o banco errado, deixando o cotovelo fora do eixo e forcando o ombro.'),

    -- ── Abdomen ──────────────────────────────────────────────────────────────
    ('Abdominal bicicleta', 7, 'Peso corporal',
     'Trabalha reto abdominal e obliquos ao mesmo tempo, sendo um dos abdominais com maior ativacao medida. Deitado com as maos ao lado da cabeca, leve o cotovelo em direcao ao joelho oposto enquanto estende a outra perna, alternando em ritmo controlado. ERRO COMUM: fazer rapido demais e puxar o pescoco com as maos — o movimento deve ser lento e vir do tronco.'),

    ('Russian twist', 7, 'Peso corporal',
     'Foca nos obliquos atraves da rotacao do tronco. Sentado com joelhos flexionados e tronco inclinado a cerca de 45 graus, gire o tronco de um lado ao outro mantendo o abdomen contraido. Para dificultar, eleve os pes do chao ou segure um peso. ERRO COMUM: girar apenas os bracos — a rotacao tem que vir do tronco, com os ombros acompanhando.'),

    ('Abdominal canivete', 7, 'Peso corporal',
     'Trabalha as porcoes superior e inferior do abdomen ao mesmo tempo. Deitado com bracos estendidos acima da cabeca e pernas retas, eleve tronco e pernas simultaneamente tentando tocar os pes, formando um V. Desca controlado sem encostar totalmente no chao. ERRO COMUM: usar impulso para subir — se nao conseguir, flexione os joelhos para facilitar.'),

    ('Roda abdominal', 7, 'Peso corporal',
     'Um dos exercicios mais desafiadores para o core, trabalhando o abdomen na funcao de resistir a extensao da coluna. Ajoelhado, segure a roda e avance devagar mantendo o abdomen contraido e a lombar NEUTRA. Volte puxando com o abdomen. Comece com amplitude curta. ERRO COMUM: avancar alem do controle e deixar a lombar arquear, o que gera dor lombar.'),

    ('Escalador', 7, 'Peso corporal',
     'Combina trabalho de core com condicionamento cardiovascular. Na posicao de prancha alta, traga um joelho de cada vez em direcao ao peito em ritmo acelerado, mantendo o quadril baixo e o corpo alinhado. Excelente para finalizar o treino. ERRO COMUM: elevar o quadril conforme cansa, o que tira a tensao do abdomen.')
) AS v("Nome", "GrupoMuscular", "Equipamento", "Descricao")
WHERE NOT EXISTS (
    SELECT 1 FROM "Exercicios" e WHERE e."Nome" = v."Nome"
);
