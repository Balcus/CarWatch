import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import { FC, useEffect, useState } from "react";
import { ReportApiClient } from "../../api/clients/ReportApiClient";
import { ReportModel } from "../../api/models/ReportModel";
import L from "leaflet";

delete (L.Icon.Default.prototype as any)._getIconUrl;

L.Icon.Default.mergeOptions({
  iconRetinaUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png",
  iconUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png",
  shadowUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png",
});

export const ReportMap: FC = () => {
  const [reports, setReports] = useState<ReportModel[]>([]);
  const fetchReports = async () => {
    try {
      const res = await ReportApiClient.getAllAsync();
      setReports(res);
    } catch {}
  };

  useEffect(() => {
    fetchReports();
  }, []);

  return (
    <MapContainer
      center={[45.75372, 21.22571]}
      zoom={13}
      style={{ height: "1200px", width: "100%" }}
    >
      <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
      {reports.map((report, index) => {
        return (
          <Marker key={index} position={[report.latitude, report.longitude]}>
            <Popup>{report.description}</Popup>
          </Marker>
        );
      })}
    </MapContainer>
  );
};
