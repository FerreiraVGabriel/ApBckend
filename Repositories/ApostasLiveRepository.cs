
using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoGabrielAPI.Interfaces;
using ProjetoGabrielAPI.Models;
using ProjetoGabrielAPI.Shared;

namespace ProjetoGabrielAPI.Repositories
{
    public class ApostasLiveRepository : IApostasLiveRepository
    {
        private readonly DataContext _context;
        private readonly UtilsProject _utilsProject;

        public ApostasLiveRepository(DataContext context, UtilsProject utilsProject)
        {
            _context = context;
            _utilsProject = utilsProject;
        }

        public void Create(ApostasLive apostasLive)
        {
            _context.Add(apostasLive);
            _context.SaveChanges();
            
        }

        public List<ApostasLive> Read()
        {
            return _context.ApostasLive.ToList();
            
        }

        public List<ApostasLive> Read(int mercadoId)
        {
            return _context.ApostasLive.Where(x=>x.Mercados_id == mercadoId).ToList();          
        }

        public List<ApostasLiveComTempo> GetEstatisticasApostasLive(List<ApostasLive> apostasLive){
            List<TempoJogo> listTempoJogo = GetTempoJogo();
            List<ApostasLiveComTempo> returnListApostaLiveComTempo = new List<ApostasLiveComTempo>();
            ApostasLiveComTempo listApostasLiveComTempo = new ApostasLiveComTempo();
            foreach(var tempoJogo in listTempoJogo){
                listApostasLiveComTempo = GetTituloEApostasLiveTempo(tempoJogo, apostasLive);
                if(listApostasLiveComTempo.ApostasLive.Any()){
                     listApostasLiveComTempo.EstatisticasApostasLive = new List<EstatisticasApostasLive>();
                    listApostasLiveComTempo.EstatisticasApostasLive = GetApostasLiveComTempo(listApostasLiveComTempo.ApostasLive);
                    returnListApostaLiveComTempo.Add(listApostasLiveComTempo);
                }

            }
            return returnListApostaLiveComTempo;
        }

        public ApostasLiveComTempo GetTituloEApostasLiveTempo(TempoJogo tempoJogo, List<ApostasLive> apostasLive){
            ApostasLiveComTempo listApostasLiveComTempo = new ApostasLiveComTempo();
            listApostasLiveComTempo.Titulo = tempoJogo.Titulo;
            listApostasLiveComTempo.ApostasLive = apostasLive.Where(x=>x.Tempo >= tempoJogo.TempoInicial && x.Tempo <= tempoJogo.TempoFinal).ToList();
            return listApostasLiveComTempo;
        }

        public List<EstatisticasApostasLive> GetApostasLiveComTempo (List<ApostasLive> apostasLives){
            List<EstatisticasApostasLive> listEstatisticasApostasLive = new List<EstatisticasApostasLive>();
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsMh1(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsMh2(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsMh3(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsEXG(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsAPM1(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsAPM2(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsCA(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsCFA(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsPosseBola(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsAtaques(apostasLives));
            listEstatisticasApostasLive.Add(GetLiveBetStatisticsAtaquesPerigosos(apostasLives));
            return listEstatisticasApostasLive;
        }

        public List<TempoJogo> GetTempoJogo(){
            return new List<TempoJogo>() { 
                new TempoJogo(){ Titulo = "Tempo de 0 a 5", TempoInicial=0, TempoFinal=5},
                new TempoJogo(){ Titulo = "Tempo de 6 a 10", TempoInicial=6, TempoFinal=10},
                new TempoJogo(){ Titulo = "Tempo de 11 a 15", TempoInicial=11, TempoFinal=15},
                new TempoJogo(){ Titulo = "Tempo de 16 a 20", TempoInicial=16, TempoFinal=20},
                new TempoJogo(){ Titulo = "Tempo de 25 a 30", TempoInicial=25, TempoFinal=30},
                new TempoJogo(){ Titulo = "Tempo de 31 a 35", TempoInicial=31, TempoFinal=35},
                new TempoJogo(){ Titulo = "Tempo de 36 a 40", TempoInicial=36, TempoFinal=40},
                new TempoJogo(){ Titulo = "Tempo de 41 a 45", TempoInicial=41, TempoFinal=45},
                new TempoJogo(){ Titulo = "Segundo tempo", TempoInicial=46, TempoFinal=90}
            };
        }

        public EstatisticasApostasLive GetEstatisticas(List<decimal?> listValoresMaiores, List<decimal?> listValoresMenores,
                                                        List<decimal?> listValoresMaioresVencedor, List<decimal?> listValoresMenoresVencedor){
            Estatisticas estatisticas = new Estatisticas();
            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMaiores);
            estatisticasApostasLive.DesvioPadraoValoresMaior = estatisticas.DesvioPadrao;
            estatisticasApostasLive.MediaValoresMaior = estatisticas.Media;
            estatisticasApostasLive.VarianciaValoresMaior = estatisticas.Variancia;
            estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMenores);
            estatisticasApostasLive.DesvioPadraoValoresMenores = estatisticas.DesvioPadrao;
            estatisticasApostasLive.MediaValoresMenores = estatisticas.Media;
            estatisticasApostasLive.VarianciaValoresMenores = estatisticas.Variancia;
            if(listValoresMaioresVencedor != null && listValoresMenoresVencedor != null){
                estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMaiores);
                estatisticasApostasLive.DesvioPadraoValoresMaiorVencedor = estatisticas.DesvioPadrao;
                estatisticasApostasLive.MediaValoresMaiorVencedor = estatisticas.Media;
                estatisticasApostasLive.VarianciaValoresMaiorVencedor = estatisticas.Variancia;
                estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMenores);
                estatisticasApostasLive.DesvioPadraoValoresMenoresVencedor = estatisticas.DesvioPadrao;
                estatisticasApostasLive.MediaValoresMenoresVencedor = estatisticas.Media;
                estatisticasApostasLive.VarianciaValoresMenoresVencedor = estatisticas.Variancia;
            }

            return estatisticasApostasLive;
        }

         public EstatisticasApostasLive GetStatistics(List<decimal?> home, List<decimal?> away, List<decimal?> roi){

            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            for (int i = 0; i < home.Count(); i++){
               if(home[i] >= away[i] ){
                    listValoresMaiores.Add(home[i]);
                    listValoresMenores.Add(away[i]);
                    if(roi[i] > 0){
                        listValoresMaioresVencedor.Add(home[i]);
                        listValoresMenoresVencedor.Add(away[i]);
                    }
                }
                else{
                    listValoresMaiores.Add(away[i]);
                    listValoresMenores.Add(home[i]);
                    if(roi[i] > 0){
                        listValoresMaioresVencedor.Add(away[i]);
                        listValoresMenoresVencedor.Add(home[i]);
                    }
                }
            }

            Estatisticas estatisticas = new Estatisticas();
            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMaiores);
            estatisticasApostasLive.DesvioPadraoValoresMaior = estatisticas.DesvioPadrao;
            estatisticasApostasLive.MediaValoresMaior = estatisticas.Media;
            estatisticasApostasLive.VarianciaValoresMaior = estatisticas.Variancia;
            estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMenores);
            estatisticasApostasLive.DesvioPadraoValoresMenores = estatisticas.DesvioPadrao;
            estatisticasApostasLive.MediaValoresMenores = estatisticas.Media;
            estatisticasApostasLive.VarianciaValoresMenores = estatisticas.Variancia;
            if(listValoresMaioresVencedor != null && listValoresMenoresVencedor != null){
                estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMaiores);
                estatisticasApostasLive.DesvioPadraoValoresMaiorVencedor = estatisticas.DesvioPadrao;
                estatisticasApostasLive.MediaValoresMaiorVencedor = estatisticas.Media;
                estatisticasApostasLive.VarianciaValoresMaiorVencedor = estatisticas.Variancia;
                estatisticas = _utilsProject.calculoMediaVarianciaDesvio(listValoresMenores);
                estatisticasApostasLive.DesvioPadraoValoresMenoresVencedor = estatisticas.DesvioPadrao;
                estatisticasApostasLive.MediaValoresMenoresVencedor = estatisticas.Media;
                estatisticasApostasLive.VarianciaValoresMenoresVencedor = estatisticas.Variancia;
            }

            return estatisticasApostasLive;
        }





        public EstatisticasApostasLive GetLiveBetStatisticsMh1(List<ApostasLive> gameTimeBetList){

            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.MH1Casa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.MH1Visitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
           if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "MH1";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsMh2(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.MH2Casa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.MH2Visitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "MH2";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive  GetLiveBetStatisticsMh3(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.MH3Casa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.MH3Visitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "MH3";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsEXG(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.EXGCasa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.EXGVisitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "EXG";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsAPM1(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.APM1Casa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.APM1Visitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "APM1";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsAPM2(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.APM2Casa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.APM2Visitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "APM2";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsCA(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.CACasa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.CAVisitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "CA";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsCFA(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.CFACasa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.CFAVisitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "CFA";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsPosseBola(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.PosseBolaCasa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.PosseBolaVisitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "Posse de bola";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsAtaques(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.AtaquesCasa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.AtaquesVisitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "Ataques";

            return liveBetsStatistics;
        }

        public EstatisticasApostasLive GetLiveBetStatisticsAtaquesPerigosos(List<ApostasLive> gameTimeBetList){
            if (gameTimeBetList == null)
                return null;

            List<decimal?> home = gameTimeBetList.Select(x=>x.AtaqPerigososCasa).ToList();
            List<decimal?> away = gameTimeBetList.Select(x=>x.AtaqPerigososVisitante).ToList();
            List<decimal?> roi = gameTimeBetList.Select(x=>x.RoiStake).ToList();

            EstatisticasApostasLive liveBetsStatistics = new EstatisticasApostasLive();
            if(home[0] != null && away[0] != null && roi[0] != null)
                liveBetsStatistics = GetStatistics(home, away, roi);

            liveBetsStatistics.Titulo = "Ataques perigosos";

            return liveBetsStatistics;
        }
    }
}

