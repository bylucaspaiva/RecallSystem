export interface Recall {
  id: number;
  titulo: string;
  descricao: string;
  dataPublicacao: string;
}

export interface ExecucaoRecall {
  id: number;
  recallId: number;
  chassi: string;
  dataExecucao: string | null;
  concessionaria: string | null;
  recall: Recall;
}