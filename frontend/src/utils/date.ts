export const formatDate = (dateValue?: string | null) => {
  if (!dateValue) {
    return 'Unknown';
  }

  const datePart = dateValue.slice(0, 10);
  const [year, month, day] = datePart.split('-');

  if (!year || !month || !day) {
    return dateValue;
  }

  return `${day}/${month}/${year}`;
};