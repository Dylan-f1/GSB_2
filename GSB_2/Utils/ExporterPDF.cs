using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GSB_2.Models;

// iText 7 - Version stable pour .NET Framework
// Note : iText 9 est optimisé pour .NET Core/.NET 5+
// Pour .NET Framework, iText 7 est recommandé
using iText.Kernel.Pdf;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
// NE PAS importer iText.Layout et iText.Layout.Element directement
// pour éviter les conflits avec System.Reflection.Metadata.Document

namespace GSB_2.Utils
{
    /// <summary>
    /// Utilitaire pour exporter des documents au format PDF
    /// Version adaptée pour GSB_2
    /// </summary>
    public static class ExporterPDF
    {
        /// <summary>
        /// Génère et exporte une prescription médicale au format PDF
        /// </summary>
        /// <param name="presc">La prescription contenant les informations (numéro, date de validité)</param>
        /// <param name="patient">Le patient pour qui la prescription est créée</param>
        /// <param name="doctor">Le médecin émettant la prescription</param>
        /// <param name="meds">Liste de tuples contenant les médicaments et leurs quantités</param>
        /// <remarks>
        /// Cette méthode utilise un <see cref="SaveFileDialog"/> pour permettre à l'utilisateur de choisir l'emplacement du PDF.
        /// Elle gère également les valeurs nulles pour éviter les crashes.
        /// En cas d'erreur, une boîte de dialogue alerte l'utilisateur et l'erreur est affichée dans la console.
        /// </remarks>
        public static void ExportPrescription(
            Prescription presc,
            Patient patient,
            User doctor,
            List<(Medicine med, int quantity)> meds)
        {
            try
            {
                // BOÎTE DE DIALOGUE POUR ENREGISTRER LE FICHIER
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Enregistrer l'ordonnance";
                dialog.Filter = "PDF (*.pdf)|*.pdf";
                dialog.FileName = $"Ordonnance_{(patient?.Name ?? "Patient")}_{(patient?.Firstname ?? "")}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                string filePath = dialog.FileName;

                // RÉCUPÉRATION SÉCURISÉE DE LA DATE DE VALIDITÉ
                // Utilise directement Validity qui est un DateTime dans le modèle Prescription
                DateTime validityDate = presc?.Validity ?? DateTime.Now;

                // CRÉATION DE LA POLICE EN GRAS
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // CRÉATION DU DOCUMENT PDF
                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (iText.Layout.Document doc = new iText.Layout.Document(pdf))
                {
                    // TITRE PRINCIPAL
                    doc.Add(new iText.Layout.Element.Paragraph("Ordonnance médicale")
                        .SetFont(boldFont)
                        .SetFontSize(22)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                    doc.Add(new iText.Layout.Element.Paragraph("\n"));

                    // INFORMATIONS PATIENT & MÉDECIN
                    string patientName = (patient?.Name ?? "[Nom inconnu]") + " " + (patient?.Firstname ?? "");
                    string doctorName = (doctor?.Name ?? "[Médecin]") + " " + (doctor?.Firstname ?? "");

                    doc.Add(new iText.Layout.Element.Paragraph($"Patient : {patientName}"));
                    doc.Add(new iText.Layout.Element.Paragraph($"Médecin : Dr {doctorName}"));
                    doc.Add(new iText.Layout.Element.Paragraph($"Validité : {validityDate:dd/MM/yyyy}"));

                    doc.Add(new iText.Layout.Element.Paragraph("\n"));

                    // TABLEAU DES MÉDICAMENTS
                    // Colonnes : 80% pour Médicament, 20% pour Quantité
                    float[] widths = { 4, 1 };
                    iText.Layout.Element.Table table = new iText.Layout.Element.Table(
                        iText.Layout.Properties.UnitValue.CreatePercentArray(widths))
                        .UseAllAvailableWidth();

                    // EN-TÊTES DU TABLEAU
                    table.AddHeaderCell(new iText.Layout.Element.Cell()
                        .Add(new iText.Layout.Element.Paragraph("Médicament"))
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

                    table.AddHeaderCell(new iText.Layout.Element.Cell()
                        .Add(new iText.Layout.Element.Paragraph("Quantité"))
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

                    // LIGNES DU TABLEAU
                    foreach (var entry in meds)
                    {
                        Medicine med = entry.med;
                        int qty = entry.quantity;

                        // Gestion sécurisée des valeurs nulles
                        string medNameSafe = med?.Name ?? "[Nom manquant]";

                        // Dosage est un int dans le modèle Medicine de GSB_2, donc conversion en string
                        string dosageSafe = med != null ? med.Dosage.ToString() : "";

                        // Affichage : "Nom du médicament 500 mg"
                        string display = $"{medNameSafe} {dosageSafe} mg".Trim();

                        table.AddCell(new iText.Layout.Element.Cell()
                            .Add(new iText.Layout.Element.Paragraph(display)));

                        table.AddCell(new iText.Layout.Element.Cell()
                            .Add(new iText.Layout.Element.Paragraph(qty.ToString())));
                    }

                    doc.Add(table);

                    // ESPACE POUR LA SIGNATURE
                    doc.Add(new iText.Layout.Element.Paragraph("\n\nSignature du médecin :\n\n"));

                    doc.Close();
                }

                // OUVRIR LE FICHIER PDF AUTOMATIQUEMENT
                // ← TEMPORAIREMENT DÉSACTIVÉ POUR DEBUG
                /* 
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
                */

                MessageBox.Show($"PDF créé avec succès !\n\nEmplacement :\n{filePath}", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // AFFICHAGE DE L'ERREUR À L'UTILISATEUR
                MessageBox.Show(
                    "Erreur lors de la génération du PDF :\n" + ex.Message,
                    "Erreur PDF",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // LOG DE L'ERREUR DANS LA CONSOLE
                Console.WriteLine("Erreur d'export PDF : " + ex.ToString());
            }
        }
    }
}