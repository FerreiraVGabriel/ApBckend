
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
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveMh1(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveMh2(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveMh3(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveEXG(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveAPM1(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveAPM2(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveCA(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveCFA(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLivePosseBole(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveAtaques(apostasLives));
            listEstatisticasApostasLive.Add(GetEstatisticasApostasLiveAtaquesPerigosos(apostasLives));
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
                new TempoJogo(){ Titulo = "Segundo tempo", TempoInicial=46, TempoFinal=null}
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

        public EstatisticasApostasLive GetEstatisticasApostasLiveMh1(List<ApostasLive> listApostasLiveTempoJogo){
            ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.MH1Casa != null && apostasLiveTempoJogo.MH1Visitante!= null){
                    if(apostasLiveTempoJogo.MH1Casa >= apostasLiveTempoJogo.MH1Visitante ){
                        listValoresMaiores.Add(apostasLiveTempoJogo.MH1Casa);
                        listValoresMenores.Add(apostasLiveTempoJogo.MH1Visitante);
                        if(apostasLiveTempoJogo.RoiStake > 0){
                            listValoresMaioresVencedor.Add(apostasLiveTempoJogo.MH1Casa);
                            listValoresMenoresVencedor.Add(apostasLiveTempoJogo.MH1Visitante);
                        }
                    }
                    else{
                        listValoresMaiores.Add(apostasLiveTempoJogo.MH1Visitante);
                        listValoresMenores.Add(apostasLiveTempoJogo.MH1Casa);
                        if(apostasLiveTempoJogo.RoiStake > 0){
                            listValoresMaioresVencedor.Add(apostasLiveTempoJogo.MH1Visitante);
                            listValoresMenoresVencedor.Add(apostasLiveTempoJogo.MH1Casa);
                        }
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "MH1";

            return estatisticasApostasLive;

        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveMh2(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.MH2Casa != null && apostasLiveTempoJogo.MH2Visitante!= null){
                    if(apostasLiveTempoJogo.MH2Casa >= apostasLiveTempoJogo.MH2Visitante ){
                        listValoresMaiores.Add(apostasLiveTempoJogo.MH2Casa);
                        listValoresMenores.Add(apostasLiveTempoJogo.MH2Visitante);
                        if(apostasLiveTempoJogo.RoiStake > 0){
                            listValoresMaioresVencedor.Add(apostasLiveTempoJogo.MH2Casa);
                            listValoresMenoresVencedor.Add(apostasLiveTempoJogo.MH2Visitante);
                        }
                    }
                    else{
                        listValoresMaiores.Add(apostasLiveTempoJogo.MH2Visitante);
                        listValoresMenores.Add(apostasLiveTempoJogo.MH2Casa);
                        if(apostasLiveTempoJogo.RoiStake > 0){
                            listValoresMaioresVencedor.Add(apostasLiveTempoJogo.MH2Visitante);
                            listValoresMenoresVencedor.Add(apostasLiveTempoJogo.MH2Casa);
                        }
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "MH2";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveMh3(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.MH3Casa != null && apostasLiveTempoJogo.MH3Visitante!= null){
                   if(apostasLiveTempoJogo.MH2Casa >= apostasLiveTempoJogo.MH2Visitante ){
                        listValoresMaiores.Add(apostasLiveTempoJogo.MH3Casa);
                        listValoresMenores.Add(apostasLiveTempoJogo.MH3Visitante);
                        if(apostasLiveTempoJogo.RoiStake > 0){
                            listValoresMaioresVencedor.Add(apostasLiveTempoJogo.MH3Casa);
                            listValoresMenoresVencedor.Add(apostasLiveTempoJogo.MH3Visitante);
                        }
                    }
                    else{
                        listValoresMaiores.Add(apostasLiveTempoJogo.MH3Visitante);
                        listValoresMenores.Add(apostasLiveTempoJogo.MH3Casa);
                        if(apostasLiveTempoJogo.RoiStake > 0){
                            listValoresMaioresVencedor.Add(apostasLiveTempoJogo.MH3Visitante);
                            listValoresMenoresVencedor.Add(apostasLiveTempoJogo.MH3Casa);
                        }
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "MH3";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveEXG(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.EXGCasa >= apostasLiveTempoJogo.EXGVisitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.EXGCasa);
                    listValoresMenores.Add(apostasLiveTempoJogo.EXGVisitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.EXGCasa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.EXGVisitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.EXGVisitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.EXGCasa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.EXGVisitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.EXGCasa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "EXG";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveAPM1(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.APM1Casa >= apostasLiveTempoJogo.APM1Visitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.APM1Casa);
                    listValoresMenores.Add(apostasLiveTempoJogo.APM1Visitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.APM1Casa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.APM1Visitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.APM1Visitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.APM1Casa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.APM1Visitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.APM1Casa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "APM1";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveAPM2(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.APM2Casa >= apostasLiveTempoJogo.APM2Visitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.APM2Casa);
                    listValoresMenores.Add(apostasLiveTempoJogo.APM2Visitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.APM2Casa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.APM2Visitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.APM2Visitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.APM2Casa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.APM2Visitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.APM2Casa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "APM2";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveCA(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.CACasa >= apostasLiveTempoJogo.CAVisitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.CACasa);
                    listValoresMenores.Add(apostasLiveTempoJogo.CAVisitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.CACasa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.CAVisitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.CAVisitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.CACasa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.CAVisitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.CACasa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "CA";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveCFA(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.CFACasa >= apostasLiveTempoJogo.CFAVisitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.CFACasa);
                    listValoresMenores.Add(apostasLiveTempoJogo.CFAVisitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.CFACasa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.CFAVisitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.CFAVisitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.CFACasa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.CFAVisitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.CFACasa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "CFA";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLivePosseBole(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.PosseBolaCasa >= apostasLiveTempoJogo.PosseBolaVisitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.PosseBolaCasa);
                    listValoresMenores.Add(apostasLiveTempoJogo.PosseBolaVisitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.PosseBolaCasa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.PosseBolaVisitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.PosseBolaVisitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.PosseBolaCasa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.PosseBolaVisitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.PosseBolaCasa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "Posso de Bola";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveAtaques(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.AtaquesCasa >= apostasLiveTempoJogo.AtaquesVisitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.AtaquesCasa);
                    listValoresMenores.Add(apostasLiveTempoJogo.AtaquesVisitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.AtaquesCasa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.AtaquesVisitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.AtaquesVisitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.AtaquesCasa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.AtaquesVisitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.AtaquesCasa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "Ataques";

            return estatisticasApostasLive;
        }

        public EstatisticasApostasLive GetEstatisticasApostasLiveAtaquesPerigosos(List<ApostasLive> listApostasLiveTempoJogo){
             ApostasLiveComTempo returnApostasLiveComTempo = new ApostasLiveComTempo();
            if (listApostasLiveTempoJogo == null)
                return null;
            
            List<decimal?> listValoresMaiores = new List<decimal?>();
            List<decimal?> listValoresMenores = new List<decimal?>();
            List<decimal?> listValoresMaioresVencedor = new List<decimal?>();
            List<decimal?> listValoresMenoresVencedor = new List<decimal?>();
            foreach(var apostasLiveTempoJogo in listApostasLiveTempoJogo){
                if(apostasLiveTempoJogo.AtaqPerigososCasa >= apostasLiveTempoJogo.AtaqPerigososVisitante ){
                    listValoresMaiores.Add(apostasLiveTempoJogo.AtaqPerigososCasa);
                    listValoresMenores.Add(apostasLiveTempoJogo.AtaqPerigososVisitante);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.AtaqPerigososCasa);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.AtaqPerigososVisitante);
                    }
                }
                else{
                    listValoresMaiores.Add(apostasLiveTempoJogo.AtaqPerigososVisitante);
                    listValoresMenores.Add(apostasLiveTempoJogo.AtaqPerigososCasa);
                    if(apostasLiveTempoJogo.RoiStake > 0){
                        listValoresMaioresVencedor.Add(apostasLiveTempoJogo.AtaqPerigososVisitante);
                        listValoresMenoresVencedor.Add(apostasLiveTempoJogo.AtaqPerigososCasa);
                    }
                }
            }

            EstatisticasApostasLive estatisticasApostasLive = new EstatisticasApostasLive();
            estatisticasApostasLive = GetEstatisticas(listValoresMaiores, listValoresMenores, listValoresMaioresVencedor, listValoresMenoresVencedor);
            estatisticasApostasLive.Titulo = "Ataques Perigosos";

            return estatisticasApostasLive;
        }
    }
}

