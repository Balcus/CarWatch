import { AiPlaygroundApiClient } from "../base/BaseApiClient";
import { ReportModel } from "../models/ReportModel";

export const ReportApiClient = {
  urlPath: "report",

  async getAllAsync(): Promise<ReportModel[]> {
    return AiPlaygroundApiClient
      .get<ReportModel[]>(this.urlPath)
      .then((response) => response.data);
  },
};
